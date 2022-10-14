using LibrariansAssistant.UserInterfaceLayer.Services.CommonServices.Interfaces;
using System.Configuration;

namespace LibrariansAssistant.UserInterfaceLayer.Services.CommonServices.Implementations;

internal sealed class ApplicationConfigurationService : IApplicationConfigurationService
{
    public string? GetAppSetting(string name) =>
        ConfigurationManager.AppSettings[name];

    public void UpdateAppSetting(string name, string value)
    {
        try
        {
            Configuration configurationFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection settings = configurationFile.AppSettings.Settings;

            if (settings[name] is null)
                settings.Add(name, value);
            else
                settings[name].Value = value;

            configurationFile.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection(configurationFile.AppSettings.SectionInformation.Name);
        }
        catch
        {
            throw;
        }
    }

    public IEnumerable<string?> GetAppSettings(IEnumerable<string> names)
    {
        if (names is null)
            throw new ArgumentNullException(nameof(names), "Names must not be null.");

        foreach (var name in names)
            yield return GetAppSetting(name);
    }

    public void UpdateAppSettings(IEnumerable<KeyValuePair<string, string>> nameValuePairs)
    {
        if (nameValuePairs is null)
            throw new ArgumentNullException(nameof(nameValuePairs), "Name-value pairs must not be null.");

        try
        {
            Configuration configurationFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection settings = configurationFile.AppSettings.Settings;

            foreach (KeyValuePair<string, string> nameValuePair in nameValuePairs)
            {
                if (settings[nameValuePair.Key] is null)
                    settings.Add(nameValuePair.Key, nameValuePair.Value);
                else
                    settings[nameValuePair.Key].Value = nameValuePair.Value;
            }

            configurationFile.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection(configurationFile.AppSettings.SectionInformation.Name);
        }
        catch
        {
            throw;
        }
    }

    public string? GetConnectionString(string name) =>
        ConfigurationManager.ConnectionStrings[name]?.ConnectionString;

    public void UpdateConnectionString(string name, string value)
    {
        try
        {
            Configuration configurationFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringSettingsCollection settings = configurationFile.ConnectionStrings.ConnectionStrings;

            settings.Remove(name);
            settings.Add(new ConnectionStringSettings(name, value));

            configurationFile.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection(configurationFile.ConnectionStrings.SectionInformation.Name);
        }
        catch
        {
            throw;
        }
    }
}