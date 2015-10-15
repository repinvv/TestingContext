namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IProvide<T1>
    {
        void Exists<T2>(string key, Func<T1, IEnumerable<T2>> srcFunc);

        void Exists<T2>(string key, Func<T1, T2> srcFunc);

        void DoesNotExist<T2>(string key, Func<T1, IEnumerable<T2>> srcFunc);

        void DoesNotExist<T2>(string key, Func<T1, T2> srcFunc);

        void Each<T2>(string key, Func<T1, IEnumerable<T2>> srcFunc);
    }

    public interface IForRoot : IProvide<TestingContext>
    {
        IFor<T1> For<T1>(string key);

        IForAll<T1> ForAll<T1>(string key);
    }

    public interface IForAll<T1>
    {
        void IsTrue(Expression<Func<IEnumerable<T1>, bool>> filter, string key = null);

        IFor<IEnumerable<T1>, T2> With<T2>(string key);

        IFor<IEnumerable<T1>, IEnumerable<T2>> ForAll<T2>(string key);
    }

    public interface IFor<T1> : IProvide<T1>
    {
        void IsTrue(Expression<Func<T1, bool>> filter, string key = null);

        IFor<T1, T2> With<T2>(string key);

        IFor<T1, IEnumerable<T2>> ForAll<T2>(string key);
    }

    public interface IFor<T1, T2>
    {
        void IsTrue(Expression<Func<T1, T2, bool>> filter, string key = null);
    }
}
