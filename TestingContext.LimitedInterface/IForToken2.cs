namespace TestingContext.LimitedInterface
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;

    public interface IForToken<T1, T2>
    {
        IFilterToken IsTrue(IDiagInfo diagInfo, Func<T1, T2, bool> filter);

        IHaveToken<T3> Exists<T3>(IDiagInfo diagInfo, Func<T1, T2, IEnumerable<T3>> srcFunc);

        IHaveToken<T3> DoesNotExist<T3>(IDiagInfo diagInfo, Func<T1, T2, IEnumerable<T3>> srcFunc);

        IHaveToken<T3> Each<T3>(IDiagInfo diagInfo, Func<T1, T2, IEnumerable<T3>> srcFunc);

        IFilterToken Group(Action<ITokenRegister> action,
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
