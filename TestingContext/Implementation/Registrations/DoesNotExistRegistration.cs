namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStore;

    internal class DoesNotExistRegistration<TDepend> : DependentRegistration<TDepend>
    {
        public DoesNotExistRegistration(string dependKey, ContextStore store) : base(dependKey, store)
        { }
    }
}
