namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Interfaces;

    internal abstract class Registration<TSource> : IRegistration<TSource>
        where TSource : class
    {
        public abstract void Provide<T>(string key, Func<TSource, IEnumerable<T>> func) where T : class;

        #region redirection methods
        public void Provide<T>(string key, Func<IEnumerable<T>> func) where T : class
        {
            Provide(key, d => func());
        }

        public void Provide<T>(string key, Func<T> func) where T : class
        {
            Provide(key, d => new[] { func() } as IEnumerable<TSource>);
        }

        public void Provide<T>(string key, Func<TSource, T> func) where T : class
        {
            Provide(key, d => new[] { func(d) } as IEnumerable<TSource>);
        }
        #endregion
    }
}
