namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IRegistration<TSource>
    {
        void Provide<T>(string key, Func<TSource, IEnumerable<T>> func) where T : class;

        void Provide<T>(string key, Func<IEnumerable<T>> func) where T : class;

        void ProvideSingle<T>(string key, Func<TSource, T> func) where T : class;

        void ProvideSingle<T>(string key, Func<T> func) where T : class;
    }
}
