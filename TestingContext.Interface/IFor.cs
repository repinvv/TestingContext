namespace TestingContext.Interface
{
    using System;
    using System.Collections.Generic;
    using TestingContext.LimitedInterface;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;

    public interface IFor<T1> : IForToken<T1>
    {
        new IFor<T1, T2> For<T2>(IHaveToken<T2> haveToken);

        new IFor<T1, IEnumerable<T2>> ForCollection<T2>(IHaveToken<T2> haveToken);

        IFor<T1, T2> For<T2>(IDiagInfo diagInfo, string name);

        IFor<T1, IEnumerable<T2>> ForCollection<T2>(IDiagInfo diagInfo, string name);


        void Exists<T2>(IDiagInfo diagInfo, string name, Func<T1, IEnumerable<T2>> srcFunc);

        void DoesNotExist<T2>(IDiagInfo diagInfo, string name, Func<T1, IEnumerable<T2>> srcFunc);

        void Each<T2>(IDiagInfo diagInfo, string name, Func<T1, IEnumerable<T2>> srcFunc);
        

        IFilterToken Not(IDiagInfo diagInfo, Action<IRegister> action);

        IFilterToken Either(IDiagInfo diagInfo, params Action<IRegister>[] actions);

        IFilterToken Xor(IDiagInfo diagInfo, Action<IRegister> action, Action<IRegister> action2);
    }
}
