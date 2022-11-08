namespace LibrariansAssistant.UserInterfaceLayer.Services.CommonServices.Interfaces;

internal interface IApplicationConfigurationService
{
    object? GetSettingValue(string name);

    void UpdateSettingValue(string name, object? value);

    IEnumerable<object?> GetSettingsValues(IEnumerable<string> names);

    void UpdateSettingsValues(IEnumerable<KeyValuePair<string, object?>> nameValuePairs);
}