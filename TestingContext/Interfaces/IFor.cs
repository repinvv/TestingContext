namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IFor<T1> : ICreateProvider<T1>
    {
        void IsTrue(Expression<Func<T1, bool>> filter, string key = null);

        IFor<T1, T2> For<T2>(string key);

        IFor<T1, IEnumerable<T2>> ForAll<T2>(string key);
    }

    public interface IFor<T1, T2>
    {
        void IsTrue(Expression<Func<T1, T2, bool>> filter, string key = null);
    }
}
