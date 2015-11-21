namespace TestingContextCore.NewInterfaces.Tokens
{
    public interface IHaveToken<T>
    {
        IToken<T> Token { get; }

        void SaveAs(string name);
    }
}
