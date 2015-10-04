namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IFor<T1>
    {
        void Filter(Expression<Func<T1, bool>> filter, string key = null);

        void ThisFilter(Expression<Func<T1, bool>> filter, string key = null);

        IWith<T1, T2> With<T2>(string key);

        IWith<T1, IEnumerable<IResolutionContext<T2>>> WithCollection<T2>(string key);
    }

    public interface IWith<T1, T2>
    {
        void Filter(Expression<Func<T1, T2, bool>> filter, string key = null);
    }
}
