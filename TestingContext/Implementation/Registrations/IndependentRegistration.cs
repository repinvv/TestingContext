namespace TestingContextCore.Implementation.Registrations
{
    using TestingContextCore.Implementation.ContextStore;

    internal class IndependentRegistration : BaseRegistration<TestingContext>
    {
        public IndependentRegistration(ContextStore store)
            : base(store)
        {
            
        }
    }
}
