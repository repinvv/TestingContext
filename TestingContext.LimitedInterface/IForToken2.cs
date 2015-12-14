namespace TestingContext.LimitedInterface
{
    using System;
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;

    public interface IForToken<T1, T2>
    {
        IFilterToken IsTrue(IDiagInfo diagInfo, Func<T1, T2, bool> filter);

        IHaveToken<T3> Exists<T3>(IDiagInfo diagInfo, Func<T1, T2, IEnumerable<T3>> srcFunc);

        IHaveToken<T3> DoesNotExist<T3>(IDiagInfo diagInfo, Func<T1, T2, IEnumerable<T3>> srcFunc);

        IHaveToken<T3> Each<T3>(IDiagInfo diagInfo, Func<T1, T2, IEnumerable<T3>> srcFunc);

        IFilterToken Not(IDiagInfo diagInfo, Action<ITokenRegister> action);

        IFilterToken Either(IDiagInfo diagInfo, params Action<ITokenRegister>[] actions);

        IFilterToken Xor(IDiagInfo diagInfo, Action<ITokenRegister> action, Action<ITokenRegister> action2);
    }
}
