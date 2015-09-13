namespace TestingContextCore.Implementation.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Resolution.ResolutionStrategy;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal class Provider<TSource, T> : IProvider
        where T : class 
        where TSource : class
    {
        private readonly ContextStore store;
        private readonly Func<TSource, IEnumerable<T>> sourceFunc;
        private readonly IResolutionStrategy strategy;
        private readonly Definition sourceDef;
        private List<IFilter> filters;
        private List<IProvider> childProviders; 

        public Provider(Definition definition, 
            Definition sourceDef,
            Func<TSource, IEnumerable<T>> sourceFunc,
            IResolutionStrategy strategy,
            ContextStore store)
        {
            this.store = store;
            Definition = definition;
            this.sourceDef = sourceDef;
            this.sourceFunc = sourceFunc;
            this.strategy = strategy;
        }

        public Definition Definition { get; }

        public IResolution Resolve(IResolutionContext parentContext)
        {
            var source = sourceFunc(parentContext.GetValue<TSource>(sourceDef))
                .Select(x => new ResolutionContext<T>(x, Definition, parentContext, Filters, ChildProviders));
            return new Resolution<T>(Definition, source, strategy);
        }

        private List<IFilter> Filters => filters = filters ?? store.GetFilters(Definition);

        private List<IProvider> ChildProviders => childProviders
            = childProviders
              ?? store.Dependencies.SafeGet(Definition, new List<INode>()).Select(x => x.Provider).ToList();
    }
}
