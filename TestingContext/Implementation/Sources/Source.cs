namespace TestingContextCore.Implementation.Sources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Interfaces;

    internal abstract class Source<TDepend, T> : ISource
    {
        private readonly ContextStore store;
        private readonly Func<TDepend, IEnumerable<T>> sourceFunc;
        private readonly EntityDefinition entityDefinition;
        private IEnumerable<IFilter> filters;

        protected Source(ContextStore store, string key, Func<TDepend, IEnumerable<T>> sourceFunc)
        {
            this.store = store;
            this.sourceFunc = sourceFunc;
            entityDefinition = new EntityDefinition(typeof(T), key);
        }

        public EntityDefinition EntityDefinition => entityDefinition;

        protected IEnumerable<IFilter> Filters => filters = filters ?? store.GetFilters(entityDefinition)
                                                                          .Where(x => x.EntityDefinitions.Where(y => !Equals(y, entityDefinition))
                                                                                       .All(y => !store.GetSource(y).IsChildOf(this)));

        public abstract IEnumerable<IResolutionContext<T1>> Resolve<T1>(string key);

        public abstract bool IsChildOf(ISource source);
    }
}
