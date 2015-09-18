namespace TestingContextCore.Implementation.ContextStorage
{
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
    }
}
