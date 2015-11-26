namespace TestingContextCore.Implementation.Tokens
{
    using TestingContextCore.Implementation.Registration;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers.Exceptions;

    internal class HaveFilterToken : IHaveFilterToken
    {
        private readonly TokenStore store;

        public HaveFilterToken(IFilterToken token, TokenStore store)
        {
            this.store = store;
            Token = token;
        }

        public IFilterToken Token { get; }

        public void SaveAs(string name)
        {
            if (store.Tokens.Get<IFilterToken>(name) != null)
            {
                throw new RegistrationException($"Value for filter with name {name} is already registered");
            }

            Token.Name = name;
            store.Tokens.Set(Token, name);
        }
    }
}
