namespace TestingContextCore.Interfaces.Tokens
{
    public interface IHaveToken<T>
    {
        IToken<T> Token { get; }

        void SaveAs(string name);
    }
}
