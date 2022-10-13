namespace LibrariansAssistant.InfranstructureLayer.Repositories.InitializationStringBuilders.Implementations.Entities;

public sealed class SqlServerIsbSettings
{
    public string ServerName { get; set; }

    public string ServerInstanceName { get; set; }

    public bool UseWindowsAuthentication { get; set; } = true;

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? DatabaseName { get; set; }

    public SqlServerIsbSettings(string serverName, string serverInstanceName) =>
        (ServerName, ServerInstanceName) = (serverName, serverInstanceName);
}