namespace TestingContextCore.NewInterfaces
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.NewInterfaces.Tokens;

    public interface IRegister<T>
    { 
        void Not(Action<IRegister<T>> action);

        void Or(params Action<IRegister<T>>[] action);

        IFor<T1> For<T1>(Func<ITestingContext, IToken<T>> getToken);

        IFor<T1> For<T1>(IHaveToken<T> token);

        IFor<IEnumerable<T1>> ForCollection<T1>(Func<ITestingContext, IToken<T>> getToken);

        IFor<IEnumerable<T1>> ForCollection<T1>(IHaveToken<T> token);
    }
}
