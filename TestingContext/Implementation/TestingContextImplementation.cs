namespace TestingContextCore.Implementation
{
    using TestingContext.Interface;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Registrations.HighLevel;
    using TestingContextCore.Implementation.Registrations.Registration0;
    using TestingContextCore.Implementation.TreeOperation;
    using TestingContextCore.PublicMembers;

    internal class TestingContextImplementation : Registration, ITestingContext
    {
        private readonly TokenStore store;

        public TestingContextImplementation(TokenStore store, InnerRegistration inner, InnerHighLevelRegistration innerHighLevel) 
            : base(store, inner, innerHighLevel)
        {
            this.store = store;
            Inversion = new Inversion(store);
        }

        #region ITestingContext members
        public int RegistrationsCount => store.Filters.Count;

        public IMatcher GetMatcher() => new Matcher(TreeOperationService.CreateTree(store).RootContext, store);

        public IRegister Priority(int priority) => RegistrationFactory.GetRegistration(store, null, priority);

        public IHaveToken<T> GetToken<T>(string name, string file, int line, string member) 
            => store.GetHaveToken<T>(DiagInfo.Create(file, line, member), name);

        public void SetToken<T>(string name, IHaveToken<T> haveToken, string file, int line, string member) 
            => store.SaveToken<T>(DiagInfo.Create(file, line, member), name, haveToken.Token);

        public IInversion Inversion { get; }

        public IStorage Storage { get; } = new Storage();
        #endregion

        
    }
}
