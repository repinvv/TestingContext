namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IFor<T1> : IProvide<T1>
    {
        void IsTrue(Expression<Func<T1, bool>> filter, string key = null);

        IFor<T1, T2> For<T2>(string key = null);

        IFor<T1, IEnumerable<T2>> ForAll<T2>(string key = null);
    }

    public interface IFor<T1, T2>
    {
        void IsTrue(Expression<Func<T1, T2, bool>> filter, string key = null);

        void Exists<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string key = null);

        void Is<T3>(Func<T1, T2, T3> srcFunc, string key = null);

        void DoesNotExist<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string key = null);

        void IsNot<T3>(Func<T1, T2, T3> srcFunc, string key = null);

        void Each<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string key = null);
    }
}
