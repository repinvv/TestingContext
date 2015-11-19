namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IRegister
    {
        IFor<T1> For<T1>(string key = null);

        IFor<IEnumerable<T1>> ForAll<T1>(string key = null);

        void Exists<T2>(Func<IEnumerable<T2>> srcFunc, string key = null);

        void Is<T2>(Func<T2> srcFunc, string key = null);
    }
}