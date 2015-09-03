namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;

    internal class ExistsRegistration<T> : BaseRegistration<T>
    {
        private readonly string key;

        public ExistsRegistration(string key)
        {
            this.key = key;
        }

        public override void Source<T1>(string key, Func<IEnumerable<T1>> func)
        { }

        public override void Source<T1>(string key, Func<T, IEnumerable<T1>> func)
        { }
    }
}
