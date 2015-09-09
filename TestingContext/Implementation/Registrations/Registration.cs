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
        where TDepend : class
    {
        public abstract void Source<T>(string key, Func<TDepend, IEnumerable<T>> func) where T : class;

        #region redirection methods
        public void Source<T>(string key, Func<IEnumerable<T>> func) where T : class
        {
            Source(key, d => func());
        }

        public void Source<T>(string key, Func<T> func) where T : class
        {
            Source(key, d => new[] { func() } as IEnumerable<TDepend>);
        }

        public void Source<T>(string key, Func<TDepend, T> func) where T : class
        {
            Source(key, d => new[] { func(d) } as IEnumerable<TDepend>);
        }
        #endregion
    }
}
