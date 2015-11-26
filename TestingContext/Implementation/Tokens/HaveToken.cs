namespace TestingContextCore.Implementation.Tokens
{
    using TestingContextCore.Implementation.Registration;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers.Exceptions;

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
            if (store.Tokens.Get<IToken<T>>(name) != null)
            {
                throw new RegistrationException($"Definition for {typeof(T).Name} {name} is already registered");
            }

            Token.Name = name;
            store.Tokens.Set(Token, name);
        }
    }
}
