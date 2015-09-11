namespace TestingContextCore.Implementation.Sources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Resolution.ResolutionStrategy;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal class Source<TDepend, T> : ISource
        where T : class 
        where TDepend : class
    {
        private readonly ContextStore store;
        private readonly Func<TDepend, IEnumerable<T>> sourceFunc;
        private readonly IResolutionStrategy strategy;
        private readonly Definition definition;

        public Source(ContextStore store, Definition definition, Func<TDepend, IEnumerable<T>> sourceFunc, IResolutionStrategy strategy)
        {
            this.store = store;
            this.definition = definition;
            this.sourceFunc = sourceFunc;
            this.strategy = strategy;
        }

        public Definition Definition => definition;

        public IResolution Resolve(IResolutionContext parentContext)
        {
            return Resolve(parentContext as IResolutionContext<TDepend>);
        }

        protected IResolution Resolve(IResolutionContext<TDepend> parentContext)
        {
            var filters = store.GetFilters(definition);
            var dependencies = store.Dependencies.SafeGet(definition, new List<INode>());
            var source = sourceFunc(parentContext.Value)
                .Select(x=> new ResolutionContext<T>(x, parentContext as IResolutionContext, filters, dependencies));
            switch (resolutionType)
            {
                case ResolutionType.Each:
                    return new EachResolution<T>(source, definition);
                case ResolutionType.DoesNotExist:
                    return new DoesNotExistResolution<T>(source, definition);
                default:
                    return new ExistsResolution<T>(source, definition);
            }
        }
    }
}
