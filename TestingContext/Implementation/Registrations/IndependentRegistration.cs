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

    internal class IndependentRegistration : Registration<TestingContext>
    {
        private readonly ContextStore store;
        private readonly TestingContext context;

        public IndependentRegistration(ContextStore store, TestingContext context)
        {
            this.store = store;
            this.context = context;
        }

        public override void Source<T>(string key, Func<TestingContext, IEnumerable<T>> srcFunc)
        {
            store.RegisterSource(new IndependentSource<T>(store, context, key, srcFunc));
        }
    }
}
