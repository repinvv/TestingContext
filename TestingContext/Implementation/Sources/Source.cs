namespace TestingContextCore.Implementation.Sources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;

    internal abstract class Source<TDepend, T> : ISource
        where T : class 
        where TDepend : class
    {
        private readonly ContextStore store;
        private readonly Func<TDepend, IEnumerable<T>> sourceFunc;
        private readonly ResolutionType resolutionType;
        private readonly EntityDefinition entityDefinition;
        private IEnumerable<IFilter> filters;

        protected Source(ContextStore store, string key, Func<TDepend, IEnumerable<T>> sourceFunc, ResolutionType resolutionType)
        {
            this.store = store;
            this.sourceFunc = sourceFunc;
            this.resolutionType = resolutionType;
            entityDefinition = new EntityDefinition(typeof(T), key);
        }

        public EntityDefinition EntityDefinition => entityDefinition;

        protected IEnumerable<IFilter> Filters => filters = filters ?? store.Filters.SafeGet(entityDefinition, () => new List<IFilter>())
                                                                          .Where(x => x.EntityDefinitions.Where(y => !Equals(y, entityDefinition))
                                                                                       .All(y => !store.GetSource(y).IsChildOf(this)));

        public abstract IEnumerable<IResolutionContext<T1>> RootResolve<T1>(string key);

        public IResolution Resolve(object parentValue)
        {
            return Resolve<T>(parentValue as TDepend);
        }

        public abstract bool IsChildOf(ISource source);

        protected IResolution<T1> Resolve<T1>(TDepend depend) where T1 : class
        {
            var source = sourceFunc(depend)
                .Select(x=> new ResolutionContext<T1>(x as T1, filters, store.Dependencies.SafeGet(entityDefinition, new List<ISource>())));
            switch (resolutionType)
            {
                    case ResolutionType.Each:
                    return new EachIndependentResolution<T1>(source);
                    case ResolutionType.Exists:
                    return new ExistsIndependentResolution<T1>(source);
                    case ResolutionType.DoesNotExist:
                    return new DoesNotExistIndependentResolution<T1>(source);
                default:
                    return new IndependentResolution<T1>(source);
            }
        }
    }
}
