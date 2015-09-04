namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStore;

    internal class DoesNotExistRegistration<T> : BaseRegistration<T> 
    {
        private readonly string dependKey;
        private readonly ContextStore store;

        public DoesNotExistRegistration(string dependKey, ContextStore store)
            : base(store)
        {
            this.dependKey = dependKey;
            this.store = store;
        }

        public override void Source<T1>(string key, Func<T, IEnumerable<T1>> func)
        { }
    }
}
