using System.Reflection;

namespace DK89.Extensions
{
    public static class ReflectionHelper
    {
        public static IEnumerable<Type> GetTypesImplementingGenericInterface(this Type genericInterface, Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract) // Only concrete classes
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericInterface));
        }
    }

}
