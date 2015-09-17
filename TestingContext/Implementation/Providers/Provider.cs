namespace TestingContextCore.Implementation.Providers
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Resolution.ResolutionStrategy;
    using TestingContextCore.Implementation.ResolutionContext;

    internal class Provider<TSource, T> : IProvider
        where T : class 
        where TSource : class
    {
        private readonly IDependency<TSource> dependency;
        private readonly ContextStore store;
        private readonly Func<TSource, IEnumerable<T>> sourceFunc;
        private readonly IResolutionStrategy strategy;
        private List<IFilter> filters;
        private List<IProvider> childProviders;

        public Provider(Definition definition, 
            IDependency<TSource> dependency,
            Func<TSource, IEnumerable<T>> sourceFunc,
            IResolutionStrategy strategy,
            ContextStore store)
        {
            this.dependency = dependency;
            this.store = store;
            Definition = definition;
            this.sourceFunc = sourceFunc;
            this.strategy = strategy;
        }

        public Definition Definition { get; }

        public IResolution Resolve(IResolutionContext parentContext)
        {
            var source = sourceFunc(dependency.GetValue(parentContext))
                .Select(x => new ResolutionContext<T>(x, Definition, parentContext, Filters, ChildProviders));
            return new Resolution<T>(Definition, source, strategy);
        }

        private List<IFilter> Filters => filters = filters ?? store.GetFilters(Definition);

        private List<IProvider> ChildProviders => childProviders
            = childProviders
              ?? store.Dependendents.SafeGet(Definition, new List<INode>()).Select(x => x.Provider).ToList();
    }
}
