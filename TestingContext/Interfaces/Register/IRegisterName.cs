namespace TestingContextCore.Interfaces.Register
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public interface IRegisterName
    {
        void Not(string name,
            Action<IRegister> action,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        void Or(string name,
            Action<IRegister> action,
            Action<IRegister> action2,
            Action<IRegister> action3 = null,
            Action<IRegister> action4 = null,
            Action<IRegister> action5 = null,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        void Xor(string name,
            Action<IRegister> action,
            Action<IRegister> action2,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IFor<T> For<T>(string name);

        IFor<IEnumerable<T>> ForCollection<T>(string name);

        void Exists<T>(string name,
            Func<IEnumerable<T>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");
    }
}
