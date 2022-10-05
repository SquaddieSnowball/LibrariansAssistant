using LibrariansAssistant.InfranstructureLayer.ObjectRelationalMappers.Entities;
using LibrariansAssistant.InfranstructureLayer.ObjectRelationalMappers.Exceptions;
using LibrariansAssistant.InfranstructureLayer.ObjectRelationalMappers.Implementations.Entities;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Data;

namespace LibrariansAssistant.InfranstructureLayer.ObjectRelationalMappers.Implementations;

public sealed class SqlServerOrm : IObjectRelationalMapper
{
    private readonly SqlConnection _sqlConnection;
    private SqlCommand _sqlCommand;
    private readonly Dictionary<Type, Type> _dependencies = new();

    public SqlServerOrm(string connectionString)
    {
        try
        {
            _sqlConnection = new SqlConnection(connectionString);
            _sqlCommand = _sqlConnection.CreateCommand();

            _sqlConnection.Open();
        }
        catch
        {
            throw;
        }
        finally
        {
            _sqlConnection?.Close();
        }
    }

    public int ExecuteNonQuery(string query, QueryExecutionSettings? executionSettings)
    {
        executionSettings ??= new();

        _sqlCommand.CommandText = query;

        if (executionSettings.IsStoredProcedure is true)
            _sqlCommand.CommandType = CommandType.StoredProcedure;

        int rowsAffected;

        try
        {
            _sqlConnection.Open();
            rowsAffected = _sqlCommand.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            _sqlConnection.Close();
            _sqlCommand = _sqlConnection.CreateCommand();
        }

        return rowsAffected;
    }

    public IEnumerable<int> ExecuteNonQueries(IEnumerable<string> queries, QueryExecutionSettings? executionSettings)
    {
        if (queries is null)
            throw new ArgumentNullException(nameof(queries), "Queries must not be null.");

        executionSettings ??= new();

        if (executionSettings.IsStoredProcedure is true)
            _sqlCommand.CommandType = CommandType.StoredProcedure;

        string[] queriesArray = queries.ToArray();
        var rowsAffectedArray = new int[queriesArray.Length];

        try
        {
            _sqlConnection.Open();

            for (var i = 0; i < queriesArray.Length; i++)
            {
                _sqlCommand.CommandText = queriesArray[i];
                rowsAffectedArray[i] = _sqlCommand.ExecuteNonQuery();
            }
        }
        catch
        {
            throw;
        }
        finally
        {
            _sqlConnection.Close();
            _sqlCommand = _sqlConnection.CreateCommand();
        }

        return rowsAffectedArray;
    }

    public IEnumerable<T> ExecuteQuery<T>(string query, QueryExecutionSettings? executionSettings, MappingSettings? mappingSettings)
    {
        executionSettings ??= new();
        mappingSettings ??= new();

        _sqlCommand.CommandText = query;

        if (executionSettings.IsStoredProcedure is true)
            _sqlCommand.CommandType = CommandType.StoredProcedure;

        var queryResults = new List<T>();
        var typeDetails = new TypeDetails(typeof(T));

        try
        {
            _sqlConnection.Open();
            SqlDataReader sqlDataReader = _sqlCommand.ExecuteReader();

            if (sqlDataReader.Read() is true)
            {
                do
                {
                    object? obj = CreateObject(typeof(T));

                    InitializeObject(obj, typeDetails, sqlDataReader, mappingSettings);

                    queryResults.Add((T)obj!);

                } while (sqlDataReader.Read() is true);
            }

            if (sqlDataReader.NextResult() is true)
            {
                int enumerableNumber = 0;

                do
                {
                    if (sqlDataReader.Read() is true)
                        foreach (T queryResult in queryResults)
                        {
                            int currentEnumerableNumber = 0;

                            InitializeObjectEnumerable(queryResult, typeDetails, sqlDataReader,
                                mappingSettings, enumerableNumber, ref currentEnumerableNumber);
                        }

                    enumerableNumber++;

                } while (sqlDataReader.NextResult() is true);
            }
        }
        catch
        {
            throw;
        }
        finally
        {
            _sqlConnection.Close();
            _sqlCommand = _sqlConnection.CreateCommand();
        }

        return queryResults;
    }

    public T ExecuteScalar<T>(string query, QueryExecutionSettings? executionSettings)
    {
        executionSettings ??= new();

        _sqlCommand.CommandText = query;

        if (executionSettings.IsStoredProcedure is true)
            _sqlCommand.CommandType = CommandType.StoredProcedure;

        T queryResult;

        try
        {
            _sqlConnection.Open();
            queryResult = (T)_sqlCommand.ExecuteScalar();
        }
        catch
        {
            throw;
        }
        finally
        {
            _sqlConnection.Close();
            _sqlCommand = _sqlConnection.CreateCommand();
        }

        return queryResult;
    }

    public IObjectRelationalMapper AddParameter<T>(string name, T value)
    {
        _sqlCommand.Parameters.Add((value is not null) ? new SqlParameter(name, value) : new SqlParameter(name, DBNull.Value));

        return this;
    }

    public void RegisterDependency(Type abstractType, Type concreteType)
    {
        if (abstractType is null)
            throw new ArgumentNullException(nameof(abstractType), "Abstract type must not be null.");

        if (concreteType is null)
            throw new ArgumentNullException(nameof(concreteType), "Concrete type must not be null.");

        try
        {
            _dependencies.Add(abstractType, concreteType);
        }
        catch (ArgumentException)
        {
            throw new ArgumentException("This dependency has already been registered.");
        }
    }

    private object? CreateObject(Type type) =>
        (_dependencies.ContainsKey(type) is true) ?
        Activator.CreateInstance(_dependencies[type]) :
        Activator.CreateInstance(type);

    private void InitializeObject(object? obj, TypeDetails typeDetails, SqlDataReader sqlDataReader,
        MappingSettings mappingSettings)
    {
        string propertyNamePrefix = string.Empty;

        if (mappingSettings.AddPropertyTypeObjectNamePrefix is true)
        {
            propertyNamePrefix =
                (_dependencies.ContainsKey(typeDetails.ObjectType) is true) ?
                _dependencies[typeDetails.ObjectType].Name :
                typeDetails.ObjectType.Name;

            if ((mappingSettings.SuppressModelPostfix is true) && (propertyNamePrefix.EndsWith("Model") is true))
                propertyNamePrefix = propertyNamePrefix.Remove(propertyNamePrefix.LastIndexOf("Model"));

            if (mappingSettings.UseSqlStylePropertiesNaming is true)
                propertyNamePrefix = propertyNamePrefix.ToLower() + '_';
        }

        foreach (PropertyDetails propertyDetails in typeDetails.PropertyDetails)
        {
            string propertyMappingName = string.Empty;

            if (mappingSettings.AddPropertyTypeObjectNamePrefix is true)
                propertyMappingName += propertyNamePrefix;

            propertyMappingName +=
                (mappingSettings.UseSqlStylePropertiesNaming is true) ?
                propertyDetails.SqlStyleName :
                propertyDetails.PropertyInfo.Name;

            if (propertyDetails.IsSimpleType is true)
            {
                try
                {
                    object propertyValue = sqlDataReader[propertyMappingName];

                    propertyDetails.PropertyInfo.SetValue(obj, (propertyValue is not DBNull) ? propertyValue : default);
                }
                catch (IndexOutOfRangeException)
                {
                    throw new MappingException($"Cannot map property named \"{propertyMappingName}\" to any column name.");
                }
            }
            else
            {
                if (propertyDetails.TypeDetails!.IsEnumerable is true)
                    continue;

                object? nestedObj = CreateObject(propertyDetails.TypeDetails.ObjectType);

                InitializeObject(nestedObj, propertyDetails.TypeDetails, sqlDataReader, mappingSettings);

                propertyDetails.PropertyInfo.SetValue(obj, nestedObj);
            }
        }
    }

    private bool InitializeObjectEnumerable(object? obj, TypeDetails typeDetails, SqlDataReader sqlDataReader,
        MappingSettings mappingSettings, int enumerableNumber, ref int currentEnumerableNumber)
    {
        foreach (PropertyDetails propertyDetails in typeDetails.PropertyDetails)
            if (propertyDetails.IsSimpleType is false)
            {
                if (propertyDetails.TypeDetails!.IsEnumerable is true)
                {
                    if (currentEnumerableNumber == enumerableNumber)
                    {
                        var enumerable =
                            (IList)CreateObject(typeof(List<>).MakeGenericType(propertyDetails.TypeDetails.ObjectType))!;
                        int elementNumber;

                        do
                        {
                            try
                            {
                                elementNumber = int.Parse(sqlDataReader["i"].ToString()!);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                throw new MappingException($"The enumerable property cannot be initialized " +
                                    $"because the column named \"i\" does not exist.");
                            }
                            catch (FormatException)
                            {
                                throw new MappingException($"The enumerable property cannot be initialized " +
                                    $"because the column named \"i\" contains a non-integer value.");
                            }

                            object? enumerabledObj = CreateObject(propertyDetails.TypeDetails.ObjectType);

                            InitializeObject(enumerabledObj, propertyDetails.TypeDetails, sqlDataReader, mappingSettings);

                            enumerable.Add(enumerabledObj);

                        } while ((sqlDataReader.Read() is true) && (elementNumber != 0));

                        propertyDetails.PropertyInfo.SetValue(obj, enumerable);

                        currentEnumerableNumber++;

                        return true;
                    }
                    else
                        currentEnumerableNumber++;
                }
                else
                {
                    object? nestedObj = propertyDetails.PropertyInfo.GetValue(obj);

                    bool isEnumerableInitialized =
                        InitializeObjectEnumerable(nestedObj, propertyDetails.TypeDetails,
                        sqlDataReader, mappingSettings, enumerableNumber, ref currentEnumerableNumber);

                    if (isEnumerableInitialized is true)
                    {
                        propertyDetails.PropertyInfo.SetValue(obj, nestedObj);

                        return true;
                    }
                }
            }

        return false;
    }
}