using LibrariansAssistant.DependenciesLayer;
using LibrariansAssistant.InfrastructureLayer.ObjectRelationalMappers;
using System.Reflection;

namespace LibrariansAssistant.InfrastructureLayer.Repositories;

/// <summary>
/// Represents the ORM dependencies configurator.
/// </summary>
internal static class OrmDependenciesConfigurator
{
    /// <summary>
    /// Configures project dependencies for the specified ORM.
    /// </summary>
    /// <param name="objectRelationalMapper">The ORM for which dependencies are to be configured.</param>
    internal static void Configure(IObjectRelationalMapper objectRelationalMapper)
    {
        MethodInfo openMethod = typeof(IObjectRelationalMapper).GetMethod("RegisterDependency")!;
        MethodInfo closedMethod;

        foreach (KeyValuePair<Type, Type> dependency in DependenciesContainer.GetAll())
        {
            closedMethod = openMethod.MakeGenericMethod(dependency.Key, dependency.Value);
            _ = closedMethod.Invoke(objectRelationalMapper, null);
        }
    }
}