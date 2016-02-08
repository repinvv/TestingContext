namespace TestingContext.LimitedInterface
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;

    public interface ITokenRegister
    {
        IForToken<T> For<T>(IHaveToken<T> haveToken);

        IForToken<IEnumerable<T>> ForCollection<T>(IHaveToken<T> haveToken);

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
