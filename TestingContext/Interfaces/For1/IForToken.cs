namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using TestingContextCore.Interfaces.Tokens;

    public interface IForToken<T1>
    {
        IFilterToken IsTrue(Expression<Func<T1, bool>> filter,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IFor<T1, T2> For<T2>(IToken<T2> token);

        IFor<T1, IEnumerable<T2>> ForCollection<T2>(IToken<T2> token);

        IToken<T2> Exists<T2>(Func<T1, IEnumerable<T2>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IToken<T2> DoesNotExist<T2>(Func<T1, IEnumerable<T2>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IToken<T2> Each<T2>(Func<T1, IEnumerable<T2>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");
    }
}
