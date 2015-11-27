namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using TestingContextCore.Interfaces.Tokens;

    public interface IRegister
    {
        IHaveFilterToken Not(Action<IRegister> action,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IHaveFilterToken Or(Action<IRegister> action,
            Action<IRegister> action2,
            Action<IRegister> action3 = null,
            Action<IRegister> action4 = null,
            Action<IRegister> action5 = null,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IHaveFilterToken Xor(Action<IRegister> action,
            Action<IRegister> action2,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IExtendedFor<T> For<T>(Func<ITestingContext, IToken<T>> getToken);
        IExtendedFor<IEnumerable<T>> ForCollection<T>(Func<ITestingContext, IToken<T>> getToken);

        #region unnamed
        IExtendedFor<T> For<T>(IHaveToken<T> haveToken);
        IExtendedFor<IEnumerable<T>> ForCollection<T>(IHaveToken<T> haveToken);
        IHaveToken<T> Exists<T>(Func<IEnumerable<T>> srcFunc,
            [CallerLineNumber] int line = 0,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "");
        IHaveToken<T> Is<T>(Func<T> srcFunc,
            [CallerLineNumber] int line = 0,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "");
        #endregion

        #region named
        IExtendedFor<T> For<T>(string name); 
        IExtendedFor<IEnumerable<T>> ForCollection<T>(string name);
        void Exists<T>(Func<IEnumerable<T>> srcFunc, 
            string name,
            [CallerLineNumber] int line = 0,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "");

        void Is<T>(Func<T> srcFunc,
            string name,
            [CallerLineNumber] int line = 0,
            [CallerFilePath] string file = "",
            [CallerMemberName] string member = "");
        #endregion
    }
}
