namespace TestingContext.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using TestingContext.LimitedInterface;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;

    public interface IFor<T1> : IForToken<T1>
    {
        new IFor<T1, T2> For<T2>(IHaveToken<T2> haveToken);

        new IFor<T1, IEnumerable<T2>> ForCollection<T2>(IHaveToken<T2> haveToken);

        IFor<T1, T2> For<T2>(string name,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IFor<T1, IEnumerable<T2>> ForCollection<T2>(string name,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");


        void Exists<T2>(IDiagInfo diagInfo, string name, Func<T1, IEnumerable<T2>> srcFunc);

        void DoesNotExist<T2>(IDiagInfo diagInfo, string name, Func<T1, IEnumerable<T2>> srcFunc);

        void Each<T2>(IDiagInfo diagInfo, string name, Func<T1, IEnumerable<T2>> srcFunc);


        IFilterToken Group(Action<IRegister> action,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IFilterToken Not(Action<IRegister> action,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "");

        IFilterToken Either(Action<IRegister> action,
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
    }
}
