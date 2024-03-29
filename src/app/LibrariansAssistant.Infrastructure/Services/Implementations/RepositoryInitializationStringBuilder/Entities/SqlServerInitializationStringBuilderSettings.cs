﻿namespace LibrariansAssistant.Infrastructure.Services.Implementations.RepositoryInitializationStringBuilder.Entities;

/// <summary>
/// Represents "SQL Server" initialization string builder settings.
/// </summary>
public sealed class SqlServerInitializationStringBuilderSettings
{
    /// <summary>
    /// Gets or sets the name of the server.
    /// </summary>
    public string ServerName { get; set; }

    /// <summary>
    /// Gets or sets the server instance name.
    /// </summary>
    public string ServerInstanceName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether Windows authentication should be used.
    /// </summary>
    public bool UseWindowsAuthentication { get; set; } = true;

    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the name of the database.
    /// </summary>
    public string? DatabaseName { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlServerInitializationStringBuilderSettings"/> class.
    /// </summary>
    /// <param name="serverName">Name of the server.</param>
    /// <param name="serverInstanceName">Server instance name.</param>
    public SqlServerInitializationStringBuilderSettings(string serverName, string serverInstanceName) =>
        (ServerName, ServerInstanceName) =
        (serverName, serverInstanceName);
}