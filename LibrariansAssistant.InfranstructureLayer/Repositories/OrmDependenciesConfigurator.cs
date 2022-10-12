using LibrariansAssistant.DependenciesLayer;
using LibrariansAssistant.InfranstructureLayer.ObjectRelationalMappers;
using System.Reflection;

namespace LibrariansAssistant.InfranstructureLayer.Repositories;

internal static class OrmDependenciesConfigurator
{
    internal static void Configure(IObjectRelationalMapper objectRelationalMapper)
    {
        MethodInfo? openMethod = typeof(IObjectRelationalMapper).GetMethod("RegisterDependency");
        MethodInfo? closedMethod;

        foreach (KeyValuePair<Type, Type> dependency in DependenciesContainer.GetAll())
        {
            closedMethod = openMethod?.MakeGenericMethod(dependency.Key, dependency.Value);
            closedMethod?.Invoke(objectRelationalMapper, null);
        }
    }
}