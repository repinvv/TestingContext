namespace TestingContextCore.Implementation.ContextStorage
{
    using System.Collections.Generic;
    using TestingContextCore.Interfaces;

    internal static class ResolutionExtension
    {
        public static IEnumerable<IResolutionContext<T>> Resolve<T>(this ContextStore store, string key)
        {
            return store.Sources[new EntityDefinition(typeof(T), key)].RootResolve<T>(key);
        }
    }
}
