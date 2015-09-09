namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IRegistration<TDepend>
    {
        void Source<T>(string key, Func<IEnumerable<T>> func) where T : class;

        void Source<T>(string key, Func<TDepend, IEnumerable<T>> func) where T : class;

        void Source<T>(string key, Func<T> func) where T : class;

        void Source<T>(string key, Func<TDepend, T> func) where T : class;
    }
}
