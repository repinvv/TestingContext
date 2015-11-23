namespace TestingContextCore.Interfaces.Tokens
{
    public interface IHaveFilterToken
    {
        IFilterToken Token { get; }

        void SaveAs(string name);
    }
}
