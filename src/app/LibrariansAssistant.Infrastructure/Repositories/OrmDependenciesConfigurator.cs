using LibrariansAssistant.Dependencies;
using LibrariansAssistant.Infrastructure.Services.Abstractions;
using System.Reflection;

namespace LibrariansAssistant.Infrastructure.Repositories;

/// <summary>
/// Represents the ORM dependencies configurator.
/// </summary>
internal static class OrmDependenciesConfigurator
{
    /// <summary>
    /// Configures project dependencies for the specified ORM.
    /// </summary>
    /// <param name="objectRelationalMapper">The ORM for which dependencies are to be configured.</param>
    public static void Configure(IObjectRelationalMapper objectRelationalMapper)
    {
        MethodInfo openMethod = typeof(IObjectRelationalMapper).GetMethod("RegisterDependency")!;
        MethodInfo closedMethod;

        foreach (KeyValuePair<Type, Type> dependency in DependenciesContainer.Dependencies)
        {
            closedMethod = openMethod.MakeGenericMethod(dependency.Key, dependency.Value);
            _ = closedMethod.Invoke(objectRelationalMapper, default);
        }
    }
}