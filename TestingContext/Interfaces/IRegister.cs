﻿namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using TestingContextCore.Interfaces.Tokens;

    public interface IRegister
    {
        void Not(Action<IRegister> action,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        void Or(Action<IRegister> action,
            Action<IRegister> action2,
            Action<IRegister> action3 = null,
            Action<IRegister> action4 = null,
            Action<IRegister> action5 = null,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        void Xor(Action<IRegister> action,
            Action<IRegister> action2,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IFor<T> For<T>(Func<ITestingContext, IToken<T>> getToken);

        IFor<IEnumerable<T>> ForCollection<T>(Func<ITestingContext, IToken<T>> getToken);

        IHaveToken<T> Exists<T>(Func<IEnumerable<T>> srcFunc);

        IHaveToken<T> Is<T>(Func<T> srcFunc);
    }
}
