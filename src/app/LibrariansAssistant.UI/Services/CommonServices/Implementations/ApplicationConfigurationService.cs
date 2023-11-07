using LibrariansAssistant.UI.Properties;
using LibrariansAssistant.UI.Services.CommonServices.Interfaces;
using System.Reflection;

namespace LibrariansAssistant.UI.Services.CommonServices.Implementations;

/// <summary>
/// Represents the application configuration service.
/// </summary>
internal sealed class ApplicationConfigurationService : IApplicationConfigurationService
{
    private readonly Settings _settings;
    private readonly IEnumerable<PropertyInfo> _settingPropertyInfos;

    /// <summary>
    /// Initializes a new instance of the ApplicationConfigurationService class.
    /// </summary>
    public ApplicationConfigurationService()
    {
        _settings = Settings.Default;
        _settingPropertyInfos = _settings.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
    }

    /// <summary>
    /// Gets the value of the application setting with the specified name.
    /// </summary>
    /// <param name="name">Name of the setting.</param>
    /// <returns>Application setting value.</returns>
    /// <exception cref="ArgumentException"></exception>
    public object? GetSettingValue(string name)
    {
        if (string.IsNullOrEmpty(name) is true)
            throw new ArgumentException($"Name must not be null or empty.", nameof(name));

        foreach (PropertyInfo settingPropertyInfo in _settingPropertyInfos)
            if (settingPropertyInfo.Name.Equals(name, StringComparison.Ordinal) is true)
                return settingPropertyInfo.GetValue(_settings);

        throw new ArgumentException($"The setting with the \"{name}\" name does not exist.", nameof(name));
    }

    /// <summary>
    /// Updates the application setting with the specified name.
    /// </summary>
    /// <param name="name">Name of the setting.</param>
    /// <param name="value">Setting value.</param>
    /// <exception cref="ArgumentException"></exception>
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

    /// <summary>
    /// Gets the values of the application settings with the specified names.
    /// </summary>
    /// <param name="names">Names of the setting.</param>
    /// <returns>Application settings values.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public IEnumerable<object?> GetSettingsValues(IEnumerable<string> names)
    {
        if (names is null)
            throw new ArgumentNullException(nameof(names), "Names must not be null.");

        foreach (string name in names)
            yield return GetSettingValue(name);
    }

    /// <summary>
    /// Updates the application settings with the specified names.
    /// </summary>
    /// <param name="nameValuePairs">Name-value pairs.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
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