namespace TestingContextCore.Implementation.Tokens
{
    using TestingContextCore.Implementation.Registration;
    using TestingContextCore.Interfaces.Tokens;

    internal class HaveToken<T> : IHaveToken<T>
    {
        private readonly TokenStore store;

        public HaveToken(IToken<T> token, TokenStore store)
        {
            this.store = store;
            Token = token;
        }

        public IToken<T> Token { get; }

        public void SaveAs(string name)
        {
            Token.Name = name;
            store.Tokens.Set(Token, name);
        }
    }
}
