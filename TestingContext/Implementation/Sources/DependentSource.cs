namespace TestingContextCore.Implementation.Sources
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Interfaces;

    internal abstract class DependentSource<TDepend, T> : Source<TDepend, T>
    {
        private readonly ContextStore store;
        private readonly EntityDefinition parentDefinition;
        private ISource parent;

        protected DependentSource(ContextStore store, string dependKey, string key, Func<TDepend, IEnumerable<T>> sourceFunc)
            : base(store, key, sourceFunc)
        {
            this.store = store;
            parentDefinition = new EntityDefinition(typeof(TDepend), dependKey);
        }

        public override IEnumerable<IResolutionContext<T1>> Resolve<T1>(string key)
        {
            return Parent.Resolve<T1>(key);
        }

        public override bool IsChildOf(ISource source)
        {
            return Parent == source || Parent.IsChildOf(source);
        }

        private ISource Parent => parent = parent ?? store.GetSource(parentDefinition);
    }
}
