namespace TestingContextCore.Interfaces.Tokens
{
    public interface IHaveFilterToken
    {
        IFilterToken Toket { get; }

        void SaveAs(string name);
    }
}
