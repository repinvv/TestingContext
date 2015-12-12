namespace TestingContextCore.Implementation.Registrations.HighLevel
{
    using System;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.PublicMembers;

    internal class HighLevelRegistrations
    {
        private readonly InnerHighLevelRegistration inner;

        public HighLevelRegistrations(InnerHighLevelRegistration inner)
        {
            this.inner = inner;
        }

        public IFilterToken Not(Action<ITokenRegister> action, string file, int line, string member)
            => inner.Not(action, DiagInfo.Create(file, line, member));

        public IFilterToken Either(Action<ITokenRegister> action, Action<ITokenRegister> action2,
                               Action<ITokenRegister> action3, Action<ITokenRegister> action4,
                               Action<ITokenRegister> action5, string file, int line, string member)
            => inner.Either(action, action2, action3, action4, action5, DiagInfo.Create(file, line, member));

        public IFilterToken Xor(Action<ITokenRegister> action, Action<ITokenRegister> action2, string file, int line, string member)
            => inner.Xor(action, action2, DiagInfo.Create(file, line, member));

        public IFilterToken Not(Action<IRegister> action, string file, int line, string member) 
            => inner.Not(action, DiagInfo.Create(file, line, member));

        public IFilterToken Either(Action<IRegister> action, Action<IRegister> action2,
                               Action<IRegister> action3, Action<IRegister> action4,
                               Action<IRegister> action5, string file, int line, string member)
            => inner.Either(action, action2, action3, action4, action5, DiagInfo.Create(file, line, member));

        public IFilterToken Xor(Action<IRegister> action, Action<IRegister> action2, string file, int line, string member)
            => inner.Xor(action, action2, DiagInfo.Create(file, line, member));
    }
}
