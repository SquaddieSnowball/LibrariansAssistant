namespace LibrariansAssistant.Dependencies;

/// <summary>
/// Represents a dependencies container used to manage project dependencies.
/// </summary>
public static class DependenciesContainer
{
    private static readonly Dictionary<Type, Type> _dependencies = new();

    /// <summary>
    /// Gets all project dependencies.
    /// </summary>
    /// <returns>All project dependencies.</returns>
    public static IEnumerable<KeyValuePair<Type, Type>> GetAll() => _dependencies;

    /// <summary>
    /// Registers a new dependency.
    /// </summary>
    /// <typeparam name="TAbstract">Abstract type.</typeparam>
    /// <typeparam name="TConcrete">Concrete type.</typeparam>
    /// <exception cref="ArgumentException"></exception>
    public static void Register<TAbstract, TConcrete>() where TConcrete : TAbstract
    {
        try
        {
            _dependencies.Add(typeof(TAbstract), typeof(TConcrete));
        }
        catch (ArgumentException)
        {
            throw new ArgumentException("This dependency has already been registered.");
        }
    }

    /// <summary>
    /// Resolves an existing dependency.
    /// </summary>
    /// <typeparam name="T">The abstract type to get the concrete type from.</typeparam>
    /// <param name="ctorArgs">Parameters that will be passed to the constructor when creating an object of a concrete type.</param>
    /// <returns>Object of a concrete type.</returns>
    /// <exception cref="ArgumentException"></exception>
    public static T? Resolve<T>(params object?[]? ctorArgs)
    {
        Type type = typeof(T);

        if (_dependencies.ContainsKey(type) is true)
        {
            try
            {
                return (T?)Activator.CreateInstance(_dependencies[type], ctorArgs);
            }
            catch
            {
                throw;
            }
        }
        else
            throw new ArgumentException("This dependency has not yet been registered.");
    }
}