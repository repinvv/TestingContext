namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface ICreateProvider<T1>
    {
        void Exists<T2>(string key, Func<T1, IEnumerable<T2>> srcFunc);

        void Satisfies<T2>(string key, Func<T1, T2> srcFunc);

        void DoesNotExist<T2>(string key, Func<T1, IEnumerable<T2>> srcFunc);

        void DoesNotSatisfy<T2>(string key, Func<T1, T2> srcFunc);

        void Each<T2>(string key, Func<T1, IEnumerable<T2>> srcFunc);
    }
}