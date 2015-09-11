namespace TestingContextCore.Implementation.Sources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal abstract class Source<TDepend, T> : ISource
        where T : class 
        where TDepend : class
    {
        private readonly ContextStore store;
        private readonly Func<TDepend, IEnumerable<T>> sourceFunc;
        private readonly ResolutionType resolutionType;
        private readonly Definition definition;

        protected Source(ContextStore store, string key, Func<TDepend, IEnumerable<T>> sourceFunc, ResolutionType resolutionType)
        {
            this.store = store;
            this.sourceFunc = sourceFunc;
            this.resolutionType = resolutionType;
            definition = new Definition(typeof(T), key);
        }

        public Definition Definition => definition;

        public abstract ISource Root { get; }

        public abstract bool IsChildOf(ISource source);

        public IResolution Resolve(IResolutionContext parentContext)
        {
            return Resolve(parentContext as IResolutionContext<TDepend>);
        }

        protected IResolution Resolve(IResolutionContext<TDepend> parentContext)
        {
            var filters = store.GetFilters(definition);
            var dependencies = store.Dependencies.SafeGet(definition, new List<ISource>());
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
