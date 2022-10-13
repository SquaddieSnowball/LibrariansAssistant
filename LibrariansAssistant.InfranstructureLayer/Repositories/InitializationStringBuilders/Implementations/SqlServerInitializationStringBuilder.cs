using LibrariansAssistant.InfranstructureLayer.Repositories.InitializationStringBuilders.Implementations.Entities;
using Microsoft.Data.SqlClient;

namespace LibrariansAssistant.InfranstructureLayer.Repositories.InitializationStringBuilders.Implementations;

public sealed class SqlServerInitializationStringBuilder : IInitializationStringBuilder
{
    private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new();

    public SqlServerInitializationStringBuilder(SqlServerIsbSettings sqlServerIsbSettings)
    {
        if (sqlServerIsbSettings is null)
            throw new ArgumentNullException(nameof(sqlServerIsbSettings),
                "SQL Server initialization string builder settings must not be null.");

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

    public string Build() =>
        _sqlConnectionStringBuilder.ConnectionString;
}