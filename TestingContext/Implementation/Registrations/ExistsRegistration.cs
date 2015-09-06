namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStore;

    internal class ExistsRegistration<TDepend> : DependentRegistration<TDepend>
    {
        public ExistsRegistration(string dependKey, ContextStore store) : base(dependKey, store)
        { }
    }
}
