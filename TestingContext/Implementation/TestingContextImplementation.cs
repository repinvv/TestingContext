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

        public IHaveToken<T> GetToken<T>(IDiagInfo diagInfo, string name) => store.GetHaveToken<T>(diagInfo, name);

        public void SetToken<T>(IDiagInfo diagInfo, string name, IHaveToken<T> haveToken) 
            => store.SaveToken<T>(diagInfo, name, haveToken.Token);

        public IInversion Inversion { get; }

        public IStorage Storage { get; } = new Storage();
        #endregion

        
    }
}
