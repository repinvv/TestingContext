namespace TestingContextCore.Implementation.Tokens
{
    using TestingContextLimitedInterface.Tokens;

    internal class HaveToken<T> : IHaveToken<T>
    {
        public HaveToken(IToken<T> token)
        {
            Token = token;
        }

        public IToken<T> Token { get; }
    }
}
