namespace TestingContextCore.Implementation.Tokens
{
    using TestingContextCore.Interfaces.Tokens;

    internal interface IHaveToken<T>
    {
        IToken<T> Token { get; }
    }
}
