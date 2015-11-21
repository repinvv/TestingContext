namespace TestingContextCore.Implementation.Tokens
{
    using TestingContextCore.Implementation.Registration;
    using TestingContextCore.Interfaces.Tokens;

    internal class HaveToken : IHaveToken
    {
        private readonly TokenStore store;

        public HaveToken(IToken token, TokenStore store)
        {
            this.store = store;
            Token = token;
        }

        public IToken Token { get; }

        public void SaveAs(string name)
        {
            Token.Name = name;
            store.Tokens.Set(Token, name);
        }
    }
}
