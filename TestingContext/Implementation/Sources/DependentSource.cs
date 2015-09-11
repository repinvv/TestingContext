namespace TestingContextCore.Implementation.Sources
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;

    internal class DependentSource<TDepend, T> : Source<TDepend, T>
        where TDepend : class
        where T : class
    {
        private readonly ContextStore store;
        private readonly Definition parentDefinition;
        private ISource parent;

        public DependentSource(ContextStore store, string dependKey, string key, Func<TDepend, IEnumerable<T>> sourceFunc, ResolutionType resolutionType)
            : base(store, key, sourceFunc, resolutionType)
        {
            this.store = store;
            parentDefinition = new Definition(typeof(TDepend), dependKey);
        }

        public override ISource Root => Parent.Root;

        public override bool IsChildOf(ISource source)
        {
            return Parent == source || Parent.IsChildOf(source);
        }

        private ISource Parent => parent = parent ?? store.GetSource(parentDefinition);
    }
}
