namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Interfaces;

    public abstract class BaseRegistration<T> : IRegistration<T>
    {
        public abstract void Source<T1>(string key, Func<IEnumerable<T1>> func);

        public abstract void Source<T1>(string key, Func<T, IEnumerable<T1>> func);

        public void Source<T1>(string key, Func<T1> func)
        {
            Source(key, () => new[] { func() } as IEnumerable<T>);
        }

        public void Source<T1>(string key, Func<T, T1> func)
        {
            Source(key, d => new[] { func(d) } as IEnumerable<T>);
        }
    }
}
