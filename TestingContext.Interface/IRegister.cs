namespace TestingContext.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using TestingContext.LimitedInterface;

    public interface IRegister : ITokenRegister
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

        new IFor<T> For<T>(IHaveToken<T> haveToken);

        new IFor<IEnumerable<T>> ForCollection<T>(IHaveToken<T> haveToken);

        IFor<T> For<T>(string name,
                       [CallerFilePath] string file = "",
                       [CallerLineNumber] int line = 0,
                       [CallerMemberName] string member = "");

        IFor<IEnumerable<T>> ForCollection<T>(string name,
                       [CallerFilePath] string file = "",
                       [CallerLineNumber] int line = 0,
                       [CallerMemberName] string member = "");

        IHaveToken<T> Exists<T>(Expression<Func<IEnumerable<T>>> srcFunc,
                                [CallerFilePath] string file = "",
                                [CallerLineNumber] int line = 0,
                                [CallerMemberName] string member = "");

        void Exists<T>(string name,
                       Expression<Func<IEnumerable<T>>> srcFunc,
                       [CallerFilePath] string file = "",
                       [CallerLineNumber] int line = 0,
                       [CallerMemberName] string member = "");
    }
}
