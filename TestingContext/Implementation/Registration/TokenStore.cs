namespace TestingContextCore.Implementation.Registration
{
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    internal class TokenStore
    {
        public TokenStore(ITestingContext context)
        {
            Context = context;
        }

        public ITestingContext Context { get; }

        public Storage Tokens { get; } = new Storage();

        public IToken RootToken { get; } = new Token();

    }
}
