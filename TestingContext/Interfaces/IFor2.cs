namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using TestingContextCore.Interfaces.Tokens;
    
    public interface IFor<T1, T2>
    {
        IHaveToken IsTrue(Expression<Func<T1, T2, bool>> filter,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IHaveToken<T3> Exists<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc);

        IHaveToken<T3> Is<T3>(Func<T1, T2, T3> srcFunc);

        IHaveToken<T3> DoesNotExist<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc);

        IHaveToken<T3> IsNot<T3>(Func<T1, T2, T3> srcFunc);

        IHaveToken<T3> Each<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc);
    }
}
