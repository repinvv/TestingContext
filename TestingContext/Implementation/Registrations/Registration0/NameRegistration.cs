namespace TestingContextCore.Implementation.Registrations.Registration0
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Register;

    internal partial class Registration : IRegisterName
    {
        public void Not(string name, Action<IRegister> action, string file, int line, string member) 
            => store.SaveFilterToken(name, inner.Not(action, file, line, member));

        public void Or(string name,
            Action<IRegister> action,
            Action<IRegister> action2,
            Action<IRegister> action3,
            Action<IRegister> action4,
            Action<IRegister> action5,
            string file,
            int line,
            string member)
            => store.SaveFilterToken(name, inner.Or(action, action2, action3, action4, action5, file, line, member));

        public void Xor(string name, Action<IRegister> action, Action<IRegister> action2, string file, int line, string member)
        => store.SaveFilterToken(name, inner.Xor(action, action2, file, line, member));

        public IFor<T> For<T>(string name) => inner.For(new LazyHaveToken<T>(() => store.GetToken<T>(name)));

        public IFor<IEnumerable<T>> ForCollection<T>(string name) => inner.ForCollection(new LazyHaveToken<T>(() => store.GetToken<T>(name)));

        public void Exists<T>(string name, Func<IEnumerable<T>> srcFunc, string file, int line, string member)
            => store.SaveToken(name, inner.Exists(srcFunc, file, line, member));
    }
}
