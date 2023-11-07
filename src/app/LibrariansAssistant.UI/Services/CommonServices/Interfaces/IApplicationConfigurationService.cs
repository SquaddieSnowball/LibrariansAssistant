namespace LibrariansAssistant.UI.Services.CommonServices.Interfaces;

/// <summary>
/// Provides methods for managing application configuration.
/// </summary>
internal interface IApplicationConfigurationService
{
    /// <summary>
    /// Gets the value of the application setting with the specified name.
    /// </summary>
    /// <param name="name">Name of the setting.</param>
    /// <returns>Application setting value.</returns>
    object? GetSettingValue(string name);

    /// <summary>
    /// Updates the application setting with the specified name.
    /// </summary>
    /// <param name="name">Name of the setting.</param>
    /// <param name="value">Setting value.</param>
    void UpdateSettingValue(string name, object? value);

    /// <summary>
    /// Gets the values of the application settings with the specified names.
    /// </summary>
    /// <param name="names">Names of the setting.</param>
    /// <returns>Application settings values.</returns>
    IEnumerable<object?> GetSettingsValues(IEnumerable<string> names);

    /// <summary>
    /// Updates the application settings with the specified names.
    /// </summary>
    /// <param name="nameValuePairs">Name-value pairs.</param>
    void UpdateSettingsValues(IEnumerable<KeyValuePair<string, object?>> nameValuePairs);
}