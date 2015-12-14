namespace TestingContext.LimitedInterface
{
    using System;
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;

    public interface ITokenRegister
    {
        IForToken<T> For<T>(IHaveToken<T> haveToken);

        IForToken<IEnumerable<T>> ForCollection<T>(IHaveToken<T> haveToken);


        IFilterToken Not(IDiagInfo diagInfo, Action<ITokenRegister> action);

        IFilterToken Either(IDiagInfo diagInfo, params Action<ITokenRegister>[] actions);

        IFilterToken Xor(IDiagInfo diagInfo, Action<ITokenRegister> action, Action<ITokenRegister> action2);
    }
}
