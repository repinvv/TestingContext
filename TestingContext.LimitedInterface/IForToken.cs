namespace TestingContext.LimitedInterface
{
    using System;
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;

    public interface IForToken<T1>
    {
        IForToken<T1, T2> For<T2>(IHaveToken<T2> haveToken);

        IForToken<T1, IEnumerable<T2>> ForCollection<T2>(IHaveToken<T2> haveToken);

        IFilterToken IsTrue(IDiagInfo diagInfo, Func<T1, bool> filter);


        IHaveToken<T2> Exists<T2>(IDiagInfo diagInfo, Func<T1, IEnumerable<T2>> srcFunc);

        IHaveToken<T2> DoesNotExist<T2>(IDiagInfo diagInfo, Func<T1, IEnumerable<T2>> srcFunc);

        IHaveToken<T2> Each<T2>(IDiagInfo diagInfo, Func<T1, IEnumerable<T2>> srcFunc);


        IFilterToken Not(IDiagInfo diagInfo, Action<ITokenRegister> action);

        IFilterToken Either(IDiagInfo diagInfo, params Action<ITokenRegister>[] actions);

        IFilterToken Xor(IDiagInfo diagInfo, Action<ITokenRegister> action, Action<ITokenRegister> action2);
    }
}
