using LibrariansAssistant.Infrastructure.ObjectRelationalMappers.Entities;

namespace LibrariansAssistant.Infrastructure.ObjectRelationalMappers;

/// <summary>
/// Provides methods for creating an ORM.
/// </summary>
public interface IObjectRelationalMapper
{
    /// <summary>
    /// Registers a new dependency.
    /// </summary>
    /// <typeparam name="TAbstract">Abstract type.</typeparam>
    /// <typeparam name="TConcrete">Concrete type.</typeparam>
    /// <returns>The current instance of the ORM object.</returns>
    IObjectRelationalMapper RegisterDependency<TAbstract, TConcrete>() where TConcrete : TAbstract;

    /// <summary>
    /// Adds a parameter to the query.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    /// <param name="name">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    /// <returns>The current instance of the ORM object.</returns>
    IObjectRelationalMapper AddParameter<T>(string name, T value);

    /// <summary>
    /// Executes the query and returns the number of affected rows.
    /// </summary>
    /// <param name="query">Query to be executed.</param>
    /// <param name="executionSettings">Execution settings.</param>
    /// <returns>The number of rows affected.</returns>
    int ExecuteNonQuery(string query, QueryExecutionSettings? executionSettings);

    /// <summary>
    /// Executes queries and returns the numbers of affected rows.
    /// </summary>
    /// <param name="queries">Queries to be executed.</param>
    /// <param name="executionSettings">Execution settings.</param>
    /// <returns>The numbers of rows affected.</returns>
    IEnumerable<int> ExecuteNonQueries(IEnumerable<string> queries, QueryExecutionSettings? executionSettings);

    /// <summary>
    /// Executes a query and converts the query results to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to convert the query results to.</typeparam>
    /// <param name="query">Query to be executed.</param>
    /// <param name="executionSettings">Execution settings.</param>
    /// <param name="mappingSettings">Mapping settings.</param>
    /// <returns>Query results converted to the specified type.</returns>
    IEnumerable<T> ExecuteQuery<T>(string query, QueryExecutionSettings? executionSettings, MappingSettings? mappingSettings);

    /// <summary>
    /// Executes the query and returns the first column of the first row converted to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to convert the query results to.</typeparam>
    /// <param name="query">Query to be executed.</param>
    /// <param name="executionSettings">Execution settings.</param>
    /// <returns>Query result converted to the specified type.</returns>
    T ExecuteScalar<T>(string query, QueryExecutionSettings? executionSettings);
}