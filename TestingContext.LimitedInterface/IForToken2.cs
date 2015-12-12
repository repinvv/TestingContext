namespace TestingContext.LimitedInterface
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public interface IForToken<T1, T2>
    {
        IFilterToken IsTrue(Expression<Func<T1, T2, bool>> filter,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IHaveToken<T3> Exists<T3>(Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IHaveToken<T3> DoesNotExist<T3>(Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IHaveToken<T3> Each<T3>(Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IHaveToken<T3> Is<T3>(Expression<Func<T1, T2, T3>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IHaveToken<T3> IsNot<T3>(Expression<Func<T1, T2, T3>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IFilterToken Not(Action<ITokenRegister> action,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IFilterToken Either(Action<ITokenRegister> action,
            Action<ITokenRegister> action2,
            Action<ITokenRegister> action3 = null,
            Action<ITokenRegister> action4 = null,
            Action<ITokenRegister> action5 = null,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IFilterToken Xor(Action<ITokenRegister> action,
            Action<ITokenRegister> action2,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");
    }
}
