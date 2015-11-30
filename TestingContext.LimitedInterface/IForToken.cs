namespace TestingContext.LimitedInterface
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public interface IForToken<T1>
    {
        IFilterToken IsTrue(Expression<Func<T1, bool>> filter,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IForToken<T1, T2> For<T2>(IHaveToken<T2> haveToken);

        IForToken<T1, IEnumerable<T2>> ForCollection<T2>(IHaveToken<T2> haveToken);

        IHaveToken<T2> Exists<T2>(Func<T1, IEnumerable<T2>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IHaveToken<T2> DoesNotExist<T2>(Func<T1, IEnumerable<T2>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IHaveToken<T2> Each<T2>(Func<T1, IEnumerable<T2>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");
    }
}
