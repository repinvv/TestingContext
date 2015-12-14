namespace TestingContextCore.Implementation.Registrations.HighLevel
{
    using System;
    using global::TestingContext.Interface;
    using global::TestingContext.LimitedInterface;
    using global::TestingContext.LimitedInterface.Diag;
    using global::TestingContext.LimitedInterface.Tokens;

    internal class HighLevelRegistrations
    {
        private readonly InnerHighLevelRegistration inner;

        public HighLevelRegistrations(InnerHighLevelRegistration inner)
        {
            this.inner = inner;
        }

        public IFilterToken Not(IDiagInfo diagInfo, Action<ITokenRegister> action)
            => inner.Not(diagInfo, action);

        public IFilterToken Either(IDiagInfo diagInfo, params Action<ITokenRegister>[] actions)
            => inner.Either(diagInfo, actions);

        public IFilterToken Xor(IDiagInfo diagInfo, Action<ITokenRegister> action, Action<ITokenRegister> action2)
            => inner.Xor(diagInfo, action, action2);

        public IFilterToken Not(IDiagInfo diagInfo, Action<IRegister> action) 
            => inner.Not(diagInfo, action);

        public IFilterToken Either(IDiagInfo diagInfo, params Action<IRegister>[] actions)
            => inner.Either(diagInfo, actions);

        public IFilterToken Xor(IDiagInfo diagInfo, Action<IRegister> action, Action<IRegister> action2)
            => inner.Xor(diagInfo, action, action2);
    }
}
