namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRegister<T> : IProvide<T>, IGet
    {
        void Not(Expression<Action<IRegister<T>>> action);

        void Or(Expression<Action<IRegister<T>>> action);

        void ScopeBy<T1>(Action<IRegister<T1>> action, string key = null);

        IFor<T1> For<T1>(string key = null);

        IFor<IEnumerable<T1>> ForAll<T1>(string key = null);
    }
}