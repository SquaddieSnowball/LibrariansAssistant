namespace LibrariansAssistant.UserInterfaceLayer.Services.CommonServices.Interfaces;

internal interface IApplicationConfigurationService
{
    string? GetAppSetting(string name);

    void UpdateAppSetting(string name, string value);

    IEnumerable<string?> GetAppSettings(IEnumerable<string> names);

    void UpdateAppSettings(IEnumerable<KeyValuePair<string, string>> nameValuePairs);

    string? GetConnectionString(string name);

    void UpdateConnectionString(string name, string value);
}