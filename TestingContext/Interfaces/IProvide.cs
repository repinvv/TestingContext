namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IProvide<T1>
    {
        void Exists<T2>(Func<T1, IEnumerable<T2>> srcFunc, string key = null);

        void Is<T2>(Func<T1, T2> srcFunc, string key);

        void DoesNotExist<T2>(Func<T1, IEnumerable<T2>> srcFunc, string key);

        void IsNot<T2>(Func<T1, T2> srcFunc, string key);

        void Each<T2>(Func<T1, IEnumerable<T2>> srcFunc, string key);
    }
}