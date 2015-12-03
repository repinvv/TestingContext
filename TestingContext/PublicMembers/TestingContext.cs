namespace TestingContextCore.PublicMembers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::TestingContext.Interface;
    using global::TestingContext.LimitedInterface;
    using TestingContextCore.Implementation;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.PublicMembers.Exceptions;
    using static Implementation.TreeOperation.TreeOperationService;

    public class TestingContext : ITestingContext
    {
        private readonly TokenStore store;
        private readonly IRegister rootRegister;

        public TestingContext()
        {
            store = new TokenStore(this);
            Inversion = new Inversion(store);
            rootRegister = RegistrationFactory.GetRegistration(store);
        }

        public IMatcher GetMatcher()
        {
            return new Matcher(GetTree(store).RootContext, store);
        }

        #region ITestingContext members



        public IHaveToken<T> GetToken<T>(string name, string file, int line, string member)
        {
            return store.GetHaveToken<T>(name, file, line, member);
        }

        public void SetToken<T>(string name, IHaveToken<T> haveToken, string file, int line, string member)
        {
            store.SaveToken(name, haveToken.Token, file, line, member);
        }

        public IInversion Inversion { get; }

        public IStorage Storage { get; } = new Storage();
        #endregion

        #region IRegister members
        public IFilterToken Not(Action<ITokenRegister> action, string file, int line, string member)
            => rootRegister.Not(action, file, line, member);

        public IFilterToken Or(Action<ITokenRegister> action, Action<ITokenRegister> action2,
                               Action<ITokenRegister> action3, Action<ITokenRegister> action4,
                               Action<ITokenRegister> action5, string file, int line, string member)
            => rootRegister.Or(action, action2, action3, action4, action5, file, line, member);
        
        public IFilterToken Xor(Action<ITokenRegister> action, Action<ITokenRegister> action2, string file, int line, string member)
            => rootRegister.Xor(action, action2, file, line, member);

        IFor<T> IRegister.For<T>(IHaveToken<T> haveToken) => rootRegister.For(haveToken);
        IFor<IEnumerable<T>> IRegister.ForCollection<T>(IHaveToken<T> haveToken) => rootRegister.ForCollection(haveToken);
        public IFor<T> For<T>(string name, string file, int line, string member) => rootRegister.For<T>(name, file, line, member);
        public IFor<IEnumerable<T>> ForCollection<T>(string name, string file, int line, string member) 
            => rootRegister.ForCollection<T>(name, file, line, member);

        public IHaveToken<T> Exists<T>(Func<IEnumerable<T>> srcFunc, string file, int line, string member)
            => rootRegister.Exists(srcFunc, file, line, member);

        public void Exists<T>(string name, Func<IEnumerable<T>> srcFunc, string file, int line, string member)
            => rootRegister.Exists(name, srcFunc, file, line, member);

        public IFilterToken Not(Action<IRegister> action, string file, int line, string member)
            => rootRegister.Not(action, file, line, member);

        public IFilterToken Or(Action<IRegister> action, Action<IRegister> action2,
                               Action<IRegister> action3, Action<IRegister> action4,
                               Action<IRegister> action5, string file, int line, string member)
            => rootRegister.Or(action, action2, action3, action4, action5, file, line, member);

        public IFilterToken Xor(Action<IRegister> action, Action<IRegister> action2, string file, int line, string member)
            => rootRegister.Xor(action, action2, file, line, member);

        IForToken<T> ITokenRegister.For<T>(IHaveToken<T> haveToken) => rootRegister.For(haveToken);

        IForToken<IEnumerable<T>> ITokenRegister.ForCollection<T>(IHaveToken<T> haveToken) => rootRegister.ForCollection(haveToken);
        #endregion
    }
}
