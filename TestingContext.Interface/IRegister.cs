namespace TestingContext.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using TestingContext.LimitedInterface;

    public interface IRegister : ITokenRegister, IHighLevelOperations<IRegister>
    {
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
