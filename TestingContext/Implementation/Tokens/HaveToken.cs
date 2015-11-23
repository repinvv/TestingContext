namespace TestingContextCore.Implementation.Tokens
{
    using TestingContextCore.Implementation.Registration;
    using TestingContextCore.Interfaces.Tokens;

    internal class HaveToken : IHaveFilterToken
    {
        private readonly TokenStore store;

        public HaveToken(IFilterToken token, TokenStore store)
        {
            this.store = store;
            Token = token;
        }

        public IFilterToken Token { get; }

        public void SaveAs(string name)
        {
            Token.Name = name;
            store.Tokens.Set(Token, name);
        }
    }
}
