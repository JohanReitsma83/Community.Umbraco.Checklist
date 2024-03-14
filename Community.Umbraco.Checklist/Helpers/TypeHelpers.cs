using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Extensions;

namespace Community.Umbraco.Checklist.Helpers;

internal static class TypeHelpers
{
    public static Task<List<Type>> GetTypesWithAttribute<T, TAttribute>() where TAttribute : Attribute
    {
        var type = typeof(T);
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var foundTypes = assemblies.SelectMany(assembly => SearchTypes(assembly, type));

        foundTypes = foundTypes.Where(p => p.HasCustomAttribute<TAttribute>(false));

        return Task.FromResult(foundTypes.OrderBy(type => type.Name).ToList());
    }

    private static IEnumerable<Type> SearchTypes(Assembly assembly, Type type)
    {
        var types = new List<Type>();
        try
        {
            var found = assembly.GetTypes()
                .Where(type.IsAssignableFrom);
            types.AddRange(found);
        }
        catch (Exception)
        {
            //assembly can't be loaded
        }
        return types;
    }

    public static T CreateObjectOfType<T>(this IServiceProvider serviceProvider, Type type)
    {
        return (T)ActivatorUtilities.CreateInstance(serviceProvider, type);
    }
}