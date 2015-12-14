namespace TestingContext.Interface
{
    using System;
    using System.Collections.Generic;
    using TestingContext.LimitedInterface;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;

    public interface IRegister : ITokenRegister
    {
        new IFor<T> For<T>(IHaveToken<T> haveToken);

        new IFor<IEnumerable<T>> ForCollection<T>(IHaveToken<T> haveToken);

        IFor<T> For<T>(IDiagInfo diagInfo, string name);

        IFor<IEnumerable<T>> ForCollection<T>(IDiagInfo diagInfo, string name);


        IHaveToken<T> Exists<T>(IDiagInfo diagInfo, Func<IEnumerable<T>> srcFunc);

        void Exists<T>(IDiagInfo diagInfo, string name, Func<IEnumerable<T>> srcFunc);


        IFilterToken Not(IDiagInfo diagInfo, Action<IRegister> action);

        IFilterToken Either(IDiagInfo diagInfo, params Action<IRegister>[] actions);

        IFilterToken Xor(IDiagInfo diagInfo, Action<IRegister> action, Action<IRegister> action2);
    }
}
