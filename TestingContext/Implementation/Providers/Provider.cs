namespace TestingContextCore.Implementation.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.UsefulExtensions;

    internal class Provider<TSource, T> : IProvider
    {
        private readonly IDependency<TSource> dependency;
        private readonly Func<TSource, IEnumerable<T>> sourceFunc;
        private readonly TokenStore store;

        public Provider(IDependency<TSource> dependency,
            Func<TSource, IEnumerable<T>> sourceFunc,
            IFilter cvFilter,
            TokenStore store)
        {
            this.dependency = dependency;
            this.sourceFunc = sourceFunc;
            this.store = store;
            CollectionValidityFilter = cvFilter;
            Dependencies = new IDependency[] { dependency };
        }

        public IEnumerable<IDependency> Dependencies { get; }

        public IFilter CollectionValidityFilter { get; }

        public IEnumerable<IResolutionContext> Resolve(IResolutionContext parentContext, INode node)
        {
            TSource sourceValue;
            if (!dependency.TryGetValue(parentContext, out sourceValue))
            {
                return Enumerable.Empty<IResolutionContext>();
            }

            var source = sourceFunc(sourceValue) ?? Enumerable.Empty<T>();
            return source
                .Select(x => new ResolutionContext<T>(x, node, parentContext, store))
                .Cache();
        }
    }
}
