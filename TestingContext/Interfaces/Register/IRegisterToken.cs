namespace TestingContextCore.Interfaces.Register
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using TestingContextCore.Interfaces.Tokens;

    public interface IRegisterToken
    {
        IFilterToken Not(Action<IRegister> action,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IFilterToken Or(Action<IRegister> action,
            Action<IRegister> action2,
            Action<IRegister> action3 = null,
            Action<IRegister> action4 = null,
            Action<IRegister> action5 = null,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IFilterToken Xor(Action<IRegister> action,
            Action<IRegister> action2,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IFor<T> For<T>(IToken<T> token);

        IFor<IEnumerable<T>> ForCollection<T>(IToken<T> token);

        IToken<T> Exists<T>(Func<IEnumerable<T>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");
    }
}
