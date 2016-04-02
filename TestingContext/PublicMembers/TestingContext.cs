namespace TestingContextCore.PublicMembers
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Registrations.HighLevel;
    using TestingContextCore.Implementation.Registrations.Registration0;
    using TestingContextCore.Implementation.TreeOperation;
    using TestingContextInterface;
    using TestingContextLimitedInterface;
    using TestingContextLimitedInterface.Diag;
    using TestingContextLimitedInterface.Tokens;

    public class TestingContext : ITestingContext
    {
        public const int DefaultPriority = 0;
        private IRegister reg;
        private readonly TokenStore store;

        public TestingContext()
        {
            store = new TokenStore();
            Inversion = new Inversion(store);
            var inner = new InnerRegistration(store, null, DefaultPriority);
            var innerHighLevel = new InnerHighLevelRegistration(store, null, DefaultPriority);
            reg = new Registration(store, inner, innerHighLevel);
        }

        #region ITestingContext members
        public int RegistrationsCount => store.FilterRegistrations.Count;

        public IMatcher GetMatcher() => new Matcher(store.CreateTree().RootContext, store);

        public IRegister Priority(int priority) => RegistrationFactory.GetRegistration(store, null, priority);

        public IHaveToken<T> GetToken<T>(string name, string file, int line, string member) 
            => store.GetHaveToken<T>(DiagInfo.Create(file, line, member), name);

        public void SetToken<T>(string name, IHaveToken<T> haveToken, string file, int line, string member) 
            => store.SaveToken(DiagInfo.Create(file, line, member), name, haveToken.Token);

        public IInversion Inversion { get; }

        public IStorage Storage { get; } = new Storage();
        #endregion

        IForToken<T> ITokenRegister.For<T>(IHaveToken<T> haveToken) => reg.For(haveToken);

        IFor<IEnumerable<T>> IRegister.ForCollection<T>(IHaveToken<T> haveToken) => reg.ForCollection(haveToken);

        public IFor<T> For<T>(string name, string file, int line, string member) 
            => reg.For<T>(name, file, line, member);

        public IFor<IEnumerable<T>> ForCollection<T>(string name, string file, int line, string member)
            => reg.ForCollection<T>(name, file, line, member);

        public IHaveToken<T> Exists<T>(IDiagInfo diagInfo, Func<IEnumerable<T>> srcFunc)
            => reg.Exists(diagInfo, srcFunc);

        public void Exists<T>(IDiagInfo diagInfo, string name, Func<IEnumerable<T>> srcFunc)
            => reg.Exists(diagInfo, name, srcFunc);

        public IFilterToken Group(Action<IRegister> action, string file, int line, string member)
            => reg.Group(action, file, line, member);

        public IFilterToken Not(Action<IRegister> action, string file, int line, string member)
            => reg.Not(action, file, line, member);

        public IFilterToken Either(Action<IRegister> action,
            Action<IRegister> action2,
            Action<IRegister> action3,
            Action<IRegister> action4,
            Action<IRegister> action5,
            string file,
            int line,
            string member)
            => reg.Either(action, action2, action3, action4, action5, file, line, member);

        public IFilterToken Xor(Action<IRegister> action, Action<IRegister> action2, string file, int line, string member)
            => reg.Xor(action, action2, file, line, member);

        IFor<T> IRegister.For<T>(IHaveToken<T> haveToken) => reg.For(haveToken);

        IForToken<IEnumerable<T>> ITokenRegister.ForCollection<T>(IHaveToken<T> haveToken) 
            => reg.ForCollection(haveToken);

        public IFilterToken Group(Action<ITokenRegister> action, string file, int line, string member)
            => reg.Group(action, file, line, member);

        public IFilterToken Not(Action<ITokenRegister> action, string file, int line, string member)
            => reg.Not(action, file, line, member);

        public IFilterToken Either(Action<ITokenRegister> action,
            Action<ITokenRegister> action2,
            Action<ITokenRegister> action3,
            Action<ITokenRegister> action4,
            Action<ITokenRegister> action5,
            string file,
            int line,
            string member)
            => reg.Either(action, action2, action3, action4, action5, file, line, member);

        public IFilterToken Xor(Action<ITokenRegister> action,
            Action<ITokenRegister> action2,
            string file,
            int line,
            string member)
            => reg.Xor(action, action2, file, line, member);
    }
}
