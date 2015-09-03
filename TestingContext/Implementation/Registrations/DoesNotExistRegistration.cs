namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;

    public class DoesNotExistRegistration<T> : BaseRegistration<T>
    {
        private readonly string key;

        public DoesNotExistRegistration(string key)
        {
            this.key = key;
        }

        public override void Source<T1>(string key, Func<IEnumerable<T1>> func)
        { }

        public override void Source<T1>(string key, Func<T, IEnumerable<T1>> func)
        { }
    }
}
