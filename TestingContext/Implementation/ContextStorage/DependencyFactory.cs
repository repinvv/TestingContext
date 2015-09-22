namespace TestingContextCore.Implementation.ContextStorage
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Interfaces;

    internal static class DependencyFactory
    {
        public static IDependency<T> Depend<T>(this ContextStore store, Definition definition, Definition dependsOn) 
        {
            var dependency = new SingleDependency<T>(definition, dependsOn);
            if (!definition.Equals(dependsOn))
            {
                store.Dependencies.Add(dependency);
            }

            return dependency;
        }

        public static IDependency<IEnumerable<IResolutionContext<T>>> CollectionDepend<T>(this ContextStore store, Definition definition, Definition dependsOn)
        {
            var dependency = new CollectionDependency<IEnumerable<IResolutionContext<T>>, T>(definition, dependsOn, store);
            if (!definition.Equals(dependsOn))
            {
                store.Dependencies.Add(dependency);
            }

            return dependency;
        }
    }
}
