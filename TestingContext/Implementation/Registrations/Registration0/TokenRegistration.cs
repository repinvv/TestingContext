namespace TestingContextCore.Implementation.Registrations.Registration0
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Register;
    using TestingContextCore.Interfaces.Tokens;

    internal partial class Registration : IRegisterToken
    {
        public IFilterToken Not(Action<IRegister> action, string file, int line, string member) 
            => inner.Not(action, file, line, member);

        public IFilterToken Or(Action<IRegister> action,
            Action<IRegister> action2,
            Action<IRegister> action3,
            Action<IRegister> action4,
            Action<IRegister> action5,
            string file, 
            int line,
            string member) => inner.Or(action, action2, action3, action4, action5, file, line, member);

        public IFilterToken Xor(Action<IRegister> action, Action<IRegister> action2, string file, int line, string member)
            => inner.Xor(action, action2, file, line, member);

        public IFor<T> For<T>(IToken<T> token) => inner.For(new HaveToken<T>(token));

        public IFor<IEnumerable<T>> ForCollection<T>(IToken<T> token) => inner.ForCollection(new HaveToken<T>(token));

        public IToken<T> Exists<T>(Func<IEnumerable<T>> srcFunc, string file, int line, string member) 
            => inner.Exists(srcFunc, file, line, member);
    }
}
