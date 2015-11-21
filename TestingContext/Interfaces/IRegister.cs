namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Interfaces.Tokens;

    public interface IRegister
    { 
        void Not(Action<IRegister> action);

        void Or(params Action<IRegister>[] action);

        IFor<T> For<T>(Func<ITestingContext, IToken<T>> getToken);

        IFor<IEnumerable<T>> ForCollection<T>(Func<ITestingContext, IToken<T>> getToken);

        IHaveToken<T> Exists<T>(Func<IEnumerable<T>> srcFunc);

        IHaveToken<T> Is<T>(Func<T> srcFunc);
    }
}
