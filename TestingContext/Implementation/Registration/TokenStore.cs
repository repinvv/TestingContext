namespace TestingContextCore.Implementation.Registration
{
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    internal class TokenStore
    {
        public Storage Tokens { get; } = new Storage();

        public IToken RootToken { get; } = new Token();

    }
}
