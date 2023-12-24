using LibrariansAssistant.Infrastructure.Services.Abstractions;
using LibrariansAssistant.Infrastructure.Services.Implementations.RepositoryInitializationStringBuilder.Entities;
using Microsoft.Data.SqlClient;

namespace LibrariansAssistant.Infrastructure.Services.Implementations.RepositoryInitializationStringBuilder;

/// <summary>
/// Represents the "SQL Server" initialization string builder.
/// </summary>
public sealed class SqlServerInitializationStringBuilder : IRepositoryInitializationStringBuilder
{
    private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlServerInitializationStringBuilder"/> class.
    /// </summary>
    /// <param name="sqlServerIsbSettings">"SQL Server" initialization string builder settings.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public SqlServerInitializationStringBuilder(SqlServerInitializationStringBuilderSettings sqlServerIsbSettings)
    {
        if (sqlServerIsbSettings is null)
        {
            throw new ArgumentNullException(nameof(sqlServerIsbSettings),
                "SQL server initialization string builder settings must not be null.");
        }

        _sqlConnectionStringBuilder["Data Source"] =
            (string.IsNullOrEmpty(sqlServerIsbSettings.ServerInstanceName) is true) ?
            sqlServerIsbSettings.ServerName :
            sqlServerIsbSettings.ServerName + "\\" + sqlServerIsbSettings.ServerInstanceName;

        if (sqlServerIsbSettings.UseWindowsAuthentication is true)
            _sqlConnectionStringBuilder["Integrated Security"] = true;
        else
        {
            _sqlConnectionStringBuilder["User ID"] = sqlServerIsbSettings.UserName;
            _sqlConnectionStringBuilder["Password"] = sqlServerIsbSettings.Password;
        }

        _sqlConnectionStringBuilder["Initial Catalog"] = sqlServerIsbSettings.DatabaseName;
    }

    /// <summary>
    /// Builds an initialization string.
    /// </summary>
    /// <returns>New initialization string.</returns>
    public string Build() => _sqlConnectionStringBuilder.ConnectionString;
}