namespace TestingContextCore.Implementation.ContextStore
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Registrations;

    internal static class ResolutionExtension
    {
        public static IEnumerable<ResolutionContext<T>> Resolve<T>(this ContextStore store, string key)
        {
            var sourcesStack = new Stack<ISource>();
            var source = store.Sources[new EntityDefinition(typeof(T), key)];
            while (source.Parent != null)
            {
                sourcesStack.Push(source);
                source = source.Parent;
            }
        }
    }
}
