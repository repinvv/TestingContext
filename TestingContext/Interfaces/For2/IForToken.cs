namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using TestingContextCore.Interfaces.Tokens;

    public interface IForToken<T1, T2>
    {
        IFilterToken IsTrue(Expression<Func<T1, T2, bool>> filter,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IToken<T3> Exists<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IToken<T3> DoesNotExist<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IToken<T3> Each<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");
    }
}
