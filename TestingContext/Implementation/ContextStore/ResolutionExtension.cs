namespace TestingContextCore.Implementation.ContextStore
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;

    internal static class ResolutionExtension
    {
        public static IEnumerable<IResolutionContext<T>> Resolve<T>(this ContextStore store, string key)
        {
            return store.Sources[new EntityDefinition(typeof(T), key)].Resolve<T>(key);
        }
    }
}
