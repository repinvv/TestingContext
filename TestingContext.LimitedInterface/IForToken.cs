namespace TestingContextLimitedInterface
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using TestingContextLimitedInterface.Diag;
    using TestingContextLimitedInterface.Tokens;

    public interface IForToken<T1>
    {
        IForToken<T1, T2> For<T2>(IHaveToken<T2> haveToken);

        IForToken<T1, IEnumerable<T2>> ForCollection<T2>(IHaveToken<T2> haveToken);

        IFilterToken IsTrue(IDiagInfo diagInfo, Func<T1, bool> filter);


        IHaveToken<T2> Exists<T2>(IDiagInfo diagInfo, Func<T1, IEnumerable<T2>> srcFunc);

        IHaveToken<T2> DoesNotExist<T2>(IDiagInfo diagInfo, Func<T1, IEnumerable<T2>> srcFunc);

        IHaveToken<T2> Each<T2>(IDiagInfo diagInfo, Func<T1, IEnumerable<T2>> srcFunc);

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
