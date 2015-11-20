namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IForContext<T> : IProvide<T>, IGet
    {
        bool IsRegistered<T1>(string key);

        void Not(Expression<Action<IForContext<T>>> action);

        void Or(params Action<IForContext<T>>[] action);

        void ScopeBy<T1>(Action<IForContext<T1>> action, string key = null);

        IFor<T1> For<T1>(string key = null);

        IFor<IEnumerable<T1>> ForAll<T1>(string key = null);
    }
}