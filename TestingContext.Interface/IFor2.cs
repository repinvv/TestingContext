namespace TestingContext.Interface
{
    using System;
    using System.Collections.Generic;
    using TestingContext.LimitedInterface;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;

    public interface IFor<T1, T2> : IForToken<T1, T2>
    {
        void Exists<T3>(IDiagInfo diagInfo, string name, Func<T1, T2, IEnumerable<T3>> srcFunc);

        void DoesNotExist<T3>(IDiagInfo diagInfo, string name, Func<T1, T2, IEnumerable<T3>> srcFunc);

        void Each<T3>(IDiagInfo diagInfo, string name, Func<T1, T2, IEnumerable<T3>> srcFunc);


        IFilterToken Not(IDiagInfo diagInfo, Action<IRegister> action);

        IFilterToken Either(IDiagInfo diagInfo, params Action<IRegister>[] action);

        IFilterToken Xor(IDiagInfo diagInfo, Action<IRegister> action, Action<IRegister> action2);
    }
}
