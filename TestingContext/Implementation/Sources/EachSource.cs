namespace TestingContextCore.Implementation.Sources
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStorage;

    internal class EachSource<TDepend, T> : DependentSource<TDepend, T>
    {
        public EachSource(ContextStore store, string dependKey, string key, Func<TDepend, IEnumerable<T>> sourceFunc)
            : base(store, dependKey, key, sourceFunc)
        { }
    }
}
