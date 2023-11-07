using LibrariansAssistant.Infrastructure.ObjectRelationalMappers.Entities;
using LibrariansAssistant.Infrastructure.ObjectRelationalMappers.Exceptions;
using LibrariansAssistant.Infrastructure.ObjectRelationalMappers.Implementations.Entities;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Data;

namespace LibrariansAssistant.Infrastructure.ObjectRelationalMappers.Implementations;

/// <summary>
/// Represents the Sql Server ORM.
/// </summary>
public sealed class SqlServerOrm : IObjectRelationalMapper
{
    private readonly Dictionary<Type, Type> _dependencies = new();
    private readonly SqlConnection _sqlConnection;
    private SqlCommand _sqlCommand;

    /// <summary>
    /// Initializes a new instance of the SqlServerOrm class.
    /// </summary>
    /// <param name="connectionString">Connection string.</param>
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

    /// <summary>
    /// Registers a new dependency.
    /// </summary>
    /// <typeparam name="TAbstract">Abstract type.</typeparam>
    /// <typeparam name="TConcrete">Concrete type.</typeparam>
    /// <returns>The current instance of the ORM object.</returns>
    /// <exception cref="ArgumentException"></exception>
    public IObjectRelationalMapper RegisterDependency<TAbstract, TConcrete>() where TConcrete : TAbstract
    {
        try
        {
            _dependencies.Add(typeof(TAbstract), typeof(TConcrete));
        }
        catch (ArgumentException)
        {
            throw new ArgumentException("This dependency has already been registered.");
        }

        return this;
    }

    /// <summary>
    /// Adds a parameter to the query.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    /// <param name="name">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <returns>The current instance of the ORM object.</returns>
    public IObjectRelationalMapper AddParameter<T>(string name, T value)
    {
        try
        {
            _ = _sqlCommand.Parameters
                .Add((value is not null) ? new SqlParameter(name, value) : new SqlParameter(name, DBNull.Value));
        }
        catch
        {
            throw;
        }

        return this;
    }

    /// <summary>
    /// Executes the query and returns the number of affected rows.
    /// </summary>
    /// <param name="query">Query to be executed.</param>
    /// <param name="executionSettings">Execution settings.</param>
    /// <returns>The number of rows affected.</returns>
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

    /// <summary>
    /// Executes queries and returns the numbers of affected rows.
    /// </summary>
    /// <param name="queries">Queries to be executed.</param>
    /// <param name="executionSettings">Execution settings.</param>
    /// <returns>The numbers of rows affected.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public IEnumerable<int> ExecuteNonQueries(IEnumerable<string> queries, QueryExecutionSettings? executionSettings)
    {
        if (queries is null)
            throw new ArgumentNullException(nameof(queries), "Queries must not be null.");

        executionSettings ??= new();

        if (executionSettings.IsStoredProcedure is true)
            _sqlCommand.CommandType = CommandType.StoredProcedure;

        string[] queriesArray = queries.ToArray();
        int[] rowsAffectedArray = new int[queriesArray.Length];

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

    /// <summary>
    /// Executes a query and converts the query results to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to convert the query results to.</typeparam>
    /// <param name="query">Query to be executed.</param>
    /// <param name="executionSettings">Execution settings.</param>
    /// <param name="mappingSettings">Mapping settings.</param>
    /// <returns>Query results converted to the specified type.</returns>
    public IEnumerable<T> ExecuteQuery<T>(string query, QueryExecutionSettings? executionSettings,
        MappingSettings? mappingSettings)
    {
        executionSettings ??= new();
        mappingSettings ??= new();

        _sqlCommand.CommandText = query;

        if (executionSettings.IsStoredProcedure is true)
            _sqlCommand.CommandType = CommandType.StoredProcedure;

        List<T> queryResults = new();
        TypeDetails typeDetails = new(typeof(T));

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

    /// <summary>
    /// Executes the query and returns the first column of the first row converted to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to convert the query results to.</typeparam>
    /// <param name="query">Query to be executed.</param>
    /// <param name="executionSettings">Execution settings.</param>
    /// <returns>Query result converted to the specified type.</returns>
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

    private object? CreateObject(Type type) =>
        (_dependencies.ContainsKey(type) is true) ?
        Activator.CreateInstance(_dependencies[type]) :
        Activator.CreateInstance(type);

    private void InitializeObject(object? obj, TypeDetails typeDetails,
        SqlDataReader sqlDataReader, MappingSettings mappingSettings)
    {
        string propertyNamePrefix = string.Empty;

        if (mappingSettings.AddPropertyTypeObjectNamePrefix is true)
        {
            propertyNamePrefix =
                (_dependencies.ContainsKey(typeDetails.ObjectType) is true) ?
                _dependencies[typeDetails.ObjectType].Name :
                typeDetails.ObjectType.Name;

            if ((mappingSettings.SuppressModelInPrefix is true) && (propertyNamePrefix.EndsWith("Model") is true))
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
                    throw new MappingException($"Cannot find a column named \"{propertyMappingName}\" " +
                        $"to initialize a property named \"{propertyDetails.PropertyInfo.Name}\".");
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
                        _ = GetEnumerableElementNumber(sqlDataReader);

                        IList enumerable =
                            (IList)CreateObject(typeof(List<>).MakeGenericType(propertyDetails.TypeDetails.ObjectType))!;

                        do
                        {
                            object? enumerabledObj = CreateObject(propertyDetails.TypeDetails.ObjectType);

                            InitializeObject(enumerabledObj, propertyDetails.TypeDetails, sqlDataReader, mappingSettings);

                            _ = enumerable.Add(enumerabledObj);

                        } while ((sqlDataReader.Read() is true) && (GetEnumerableElementNumber(sqlDataReader) != 0));

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
                        InitializeObjectEnumerable(nestedObj, propertyDetails.TypeDetails, sqlDataReader,
                        mappingSettings, enumerableNumber, ref currentEnumerableNumber);

                    if (isEnumerableInitialized is true)
                    {
                        propertyDetails.PropertyInfo.SetValue(obj, nestedObj);

                        return true;
                    }
                }
            }

        return false;
    }

    private static int GetEnumerableElementNumber(SqlDataReader sqlDataReader)
    {
        try
        {
            return int.Parse(sqlDataReader["i"].ToString()!);
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
    }
}