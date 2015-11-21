namespace TestingContextCore.NewInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using TestingContextCore.Interfaces;
    using TestingContextCore.NewInterfaces.Tokens;

    public interface IFor<T1>
    {
        IHaveFilterToken IsTrue(Expression<Func<T1, bool>> filter);

        IFor<T1, T2> For<T2>(Func<ITestingContext, IToken<T2>> getToken);

        IFor<T1, T2> For<T2>(IHaveToken<T2> token);

        IFor<T1, IEnumerable<T2>> ForAll<T2>(Func<ITestingContext, IToken<T2>> getToken);

        IFor<T1, IEnumerable<T2>> ForAll<T2>(IHaveToken<T2> token);

        IHaveToken<T2> Exists<T2>(Func<T1, IEnumerable<T2>> srcFunc);

        IHaveToken<T2> Is<T2>(Func<T1, T2> srcFunc);

        IHaveToken<T2> DoesNotExist<T2>(Func<T1, IEnumerable<T2>> srcFunc);

        IHaveToken<T2> IsNot<T2>(Func<T1, T2> srcFunc);

        IHaveToken<T2> Each<T2>(Func<T1, IEnumerable<T2>> srcFunc);
    }
}
