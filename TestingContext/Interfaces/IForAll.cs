namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IForAll<T1>
    {
        void IsTrue(Expression<Func<IEnumerable<T1>, bool>> filter, string key = null);

        IFor<IEnumerable<T1>, T2> For<T2>(string key);

        IFor<IEnumerable<T1>, IEnumerable<T2>> ForAll<T2>(string key);
    }
}