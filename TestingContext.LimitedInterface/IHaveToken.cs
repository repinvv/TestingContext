namespace TestingContext.LimitedInterface
{
    public interface IHaveToken<T>
    {
        IToken<T> Token { get; }
    }
}
