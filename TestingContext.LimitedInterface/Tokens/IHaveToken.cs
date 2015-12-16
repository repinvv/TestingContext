namespace TestingContext.LimitedInterface.Tokens
{
    public interface IHaveToken<T>
    {
        IToken<T> Token { get; }
    }
}
