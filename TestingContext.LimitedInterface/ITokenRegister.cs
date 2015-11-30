﻿namespace TestingContext.LimitedInterface
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public interface ITokenRegister
    {
        IFilterToken Not(Action<ITokenRegister> action,
                 [CallerFilePath] string file = "",
                 [CallerLineNumber] int line = 0,
                 [CallerMemberName] string member = "");

        IFilterToken Or(Action<ITokenRegister> action,
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

        IForToken<T> For<T>(IHaveToken<T> haveToken);

        IForToken<IEnumerable<T>> ForCollection<T>(IHaveToken<T> haveToken);
    }
}