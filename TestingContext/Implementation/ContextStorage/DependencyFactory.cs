namespace TestingContextCore.Implementation.ContextStorage
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Dependencies;

    internal static class DependencyFactory
    {
        public static IDependency<T> Depend<T>(this ContextStore store, Definition definition, Definition dependsOn) 
            where T : class
        {
            var dependency = new SingleDependency<T>(definition, dependsOn);
            if (!definition.Equals(dependsOn))
            {
                store.Dependencies.Add(dependency);
            }

            return dependency;
        }

        public static IDependency<IEnumerable<T>> CollectionDepend<T>(this ContextStore store, Definition definition, Definition dependsOn) where T : class
        {
            var dependency = new CollectionDependency<IEnumerable<T>, T>(definition, dependsOn);
            if (!definition.Equals(dependsOn))
            {
                store.Dependencies.Add(dependency);
            }

            return dependency;
        }
    }
}
