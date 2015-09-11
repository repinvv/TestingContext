namespace TestingContextCore.Implementation.Sources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;

    internal class RootSource<T> : Source<TestingContext, T>
        where T : class
    {
        public RootSource(ContextStore store, string key, Func<TestingContext, IEnumerable<T>> sourceFunc)
            : base(store, key, sourceFunc, ResolutionType.Independent)
        {
        }
        public override ISource Root => this;

        public override bool IsChildOf(ISource source)
        {
            return false;
        }
    }
}
