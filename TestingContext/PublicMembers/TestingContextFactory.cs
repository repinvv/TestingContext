namespace TestingContextCore.PublicMembers
{
    using TestingContext.Interface;
    using TestingContextCore.Implementation;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Registrations.HighLevel;
    using TestingContextCore.Implementation.Registrations.Registration0;

    public static class TestingContextFactory
    {
        public const int DefaultPriority = 0;
        public static ITestingContext Create()
        {
            var store = new TokenStore();
            var inner = new InnerRegistration(store, null, DefaultPriority);
            var innerHighLevel = new InnerHighLevelRegistration(store, null, DefaultPriority);
            return new TestingContextImplementation(store, inner, innerHighLevel);
        }
    }
}
