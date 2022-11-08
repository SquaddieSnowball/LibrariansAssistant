using LibrariansAssistant.UserInterfaceLayer.Properties;
using LibrariansAssistant.UserInterfaceLayer.Services.CommonServices.Interfaces;
using System.Reflection;

namespace LibrariansAssistant.UserInterfaceLayer.Services.CommonServices.Implementations;

internal sealed class ApplicationConfigurationService : IApplicationConfigurationService
{
    private readonly Settings _settings;
    private readonly IEnumerable<PropertyInfo> _settingPropertyInfos;

    public ApplicationConfigurationService()
    {
        _settings = Settings.Default;
        _settingPropertyInfos = _settings.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
    }

    public object? GetSettingValue(string name)
    {
        if (string.IsNullOrEmpty(name) is true)
            throw new ArgumentException($"Name must not be null or empty.", nameof(name));

        foreach (PropertyInfo settingPropertyInfo in _settingPropertyInfos)
            if (settingPropertyInfo.Name.Equals(name, StringComparison.Ordinal) is true)
                return settingPropertyInfo.GetValue(_settings);

        throw new ArgumentException($"The setting with the \"{name}\" name does not exist.", nameof(name));
    }

    public void UpdateSettingValue(string name, object? value)
    {
        if (string.IsNullOrEmpty(name) is true)
            throw new ArgumentException($"Name must not be null or empty.", nameof(name));

        foreach (PropertyInfo settingPropertyInfo in _settingPropertyInfos)
            if (settingPropertyInfo.Name.Equals(name, StringComparison.Ordinal) is true)
            {
                try
                {
                    settingPropertyInfo.SetValue(_settings, value);

                    _settings.Save();

                    return;
                }
                catch (ArgumentException)
                {
                    if (settingPropertyInfo.CanWrite is false)
                        throw new ArgumentException($"The \"{name}\" setting " +
                            $"cannot be updated because it is read-only.", nameof(name));

                    if (settingPropertyInfo.PropertyType != value!.GetType())
                        throw new ArgumentException($"The \"{name}\" setting " +
                            $"cannot be updated because the \"{value.GetType().FullName}\" value type " +
                            $"cannot be converted to the \"{settingPropertyInfo.PropertyType.FullName}\" setting type.",
                            nameof(value));

                    throw;
                }
                catch
                {
                    throw;
                }
            }

        throw new ArgumentException($"The setting with the \"{name}\" name does not exist.", nameof(name));
    }

    public IEnumerable<object?> GetSettingsValues(IEnumerable<string> names)
    {
        if (names is null)
            throw new ArgumentNullException(nameof(names), "Names must not be null.");

        foreach (string name in names)
            yield return GetSettingValue(name);
    }

    public void UpdateSettingsValues(IEnumerable<KeyValuePair<string, object?>> nameValuePairs)
    {
        if (nameValuePairs is null)
            throw new ArgumentNullException(nameof(nameValuePairs), "Name-value pairs must not be null.");

        foreach (KeyValuePair<string, object?> nameValuePair in nameValuePairs)
        {
            if (string.IsNullOrEmpty(nameValuePair.Key) is true)
                throw new ArgumentException($"Name must not be null or empty.", nameof(nameValuePairs));

            bool isSettingPropertyInfoFound = false;

            foreach (PropertyInfo settingPropertyInfo in _settingPropertyInfos)
                if (settingPropertyInfo.Name.Equals(nameValuePair.Key, StringComparison.Ordinal) is true)
                {
                    isSettingPropertyInfoFound = true;

                    try
                    {
                        settingPropertyInfo.SetValue(_settings, nameValuePair.Value);

                        break;
                    }
                    catch (ArgumentException)
                    {
                        if (settingPropertyInfo.CanWrite is false)
                            throw new ArgumentException($"The \"{nameValuePair.Key}\" setting " +
                                $"cannot be updated because it is read-only.", nameof(nameValuePairs));

                        if (settingPropertyInfo.PropertyType != nameValuePair.Value!.GetType())
                            throw new ArgumentException($"The \"{nameValuePair.Key}\" setting " +
                                $"cannot be updated because the \"{nameValuePair.Value.GetType().FullName}\" value type " +
                                $"cannot be converted to the \"{settingPropertyInfo.PropertyType.FullName}\" setting type.",
                                nameof(nameValuePairs));

                        throw;
                    }
                    catch
                    {
                        throw;
                    }
                }

            if (isSettingPropertyInfoFound is false)
                throw new ArgumentException($"The setting with the \"{nameValuePair.Key}\" " +
                    $"name does not exist.", nameof(nameValuePairs));
        }

        _settings.Save();
    }
}