namespace TestingContextCore.Interfaces.Tokens
{
    public interface IHaveToken
    {
        IToken Token { get; }

        void SaveAs(string name);
    }
}
