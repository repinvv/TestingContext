namespace TestingContextCore.Implementation.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.ResolutionContext;

    internal class Provider<TSource, T> : IProvider
    {
        private readonly IDependency<TSource> dependency;
        private readonly Func<TSource, IEnumerable<T>> sourceFunc;
        private readonly ProviderDetails details;
        private readonly ContextStore store;

        public Provider(IDependency<TSource> dependency,
            Func<TSource, IEnumerable<T>> sourceFunc,
            ProviderDetails details,
            ContextStore store)
        {
            this.dependency = dependency;
            this.sourceFunc = sourceFunc;
            this.details = details;
            this.store = store;
        }

        public Definition Definition => details.Definition;

        public IResolution Resolve(IResolutionContext parentContext)
        {
            TSource sourceValue;
            if (!dependency.TryGetValue(parentContext, out sourceValue))
            {
                return new EmptyResolution();
            }

            var source = sourceFunc(sourceValue) ?? Enumerable.Empty<T>();
            return new Resolution<T>(Definition, parentContext, source, details.Filters, details.CollectionFilters, details.ChildProviders, store);
        }
    }
}
