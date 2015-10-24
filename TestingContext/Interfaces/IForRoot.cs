namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IForRoot
    {
        IFor<T1> For<T1>(string key);

        IForAll<IEnumerable<T1>> ForAll<T1>(string key);

        void Items<T2>(string key, Func<IEnumerable<T2>> srcFunc);

        void Item<T2>(string key, Func<T2> srcFunc);
    }
}