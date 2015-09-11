namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Sources;
    using TestingContextCore.Interfaces;

    internal class RootRegistration : Registration<TestingContext>
    {
        private readonly ContextStore store;

        public RootRegistration(ContextStore store)
        {
            this.store = store;
        }

        public override void Source<T>(string key, Func<TestingContext, IEnumerable<T>> srcFunc)
        {
            store.RegisterSource(new RootSource<T>(store, key, srcFunc));
        }
    }
}
