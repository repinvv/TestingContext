namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IFor<T1>
    {
        void IsTrue(Expression<Func<T1, bool>> filter, string key = null);

        IFor<T1, T2> For<T2>(string key);

        IFor<T1, IEnumerable<T2>> ForAll<T2>(string key);

        void Exists<T2>(string key, Func<T1, IEnumerable<T2>> srcFunc);

        void Satisfies<T2>(string key, Func<T1, T2> srcFunc);

        void DoesNotExist<T2>(string key, Func<T1, IEnumerable<T2>> srcFunc);

        void DoesNotSatisfy<T2>(string key, Func<T1, T2> srcFunc);

        void Each<T2>(string key, Func<T1, IEnumerable<T2>> srcFunc);
    }

    public interface IFor<T1, T2>
    {
        void IsTrue(Expression<Func<T1, T2, bool>> filter, string key = null);

        void Exists<T3>(string key, Func<T1, T2, IEnumerable<T3>> srcFunc);

        void Satisfies<T3>(string key, Func<T1, T2, T3> srcFunc);

        void DoesNotExist<T3>(string key, Func<T1, T2, IEnumerable<T3>> srcFunc);

        void DoesNotSatisfy<T3>(string key, Func<T1, T2, T3> srcFunc);

        void Each<T3>(string key, Func<T1, T2, IEnumerable<T3>> srcFunc);
    }
}
