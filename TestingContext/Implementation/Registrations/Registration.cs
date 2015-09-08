namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Sources;
    using TestingContextCore.Interfaces;

    internal abstract class Registration<TDepend> : IRegistration<TDepend>
    {
        public abstract void Source<T1>(string key, Func<TDepend, IEnumerable<T1>> func);

        #region redirection methods
        public void Source<T1>(string key, Func<IEnumerable<T1>> func)
        {
            Source(key, d => func());
        }

        public void Source<T1>(string key, Func<T1> func)
        {
            Source(key, d => new[] { func() } as IEnumerable<TDepend>);
        }

        public void Source<T1>(string key, Func<TDepend, T1> func)
        {
            Source(key, d => new[] { func(d) } as IEnumerable<TDepend>);
        }
        #endregion
    }
}
