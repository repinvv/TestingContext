namespace TestingContextLimitedInterface.Tokens
{
    public interface IHaveToken<T>
    {
        IToken<T> Token { get; }
    }
}
