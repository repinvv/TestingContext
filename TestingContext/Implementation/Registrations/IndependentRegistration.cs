namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;

    internal class IndependentRegistration : BaseRegistration<TestingContext>
    {
        public override void Source<T1>(string key, Func<IEnumerable<T1>> func)
        { }

        public override void Source<T1>(string key, Func<TestingContext, IEnumerable<T1>> func)
        { }
    }
}
