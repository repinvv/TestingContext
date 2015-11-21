namespace TestingContextCore.NewInterfaces.Tokens
{
    public interface IHaveFilterToken
    {
        IFilterToken Toket { get; }

        void SaveAs(string name);
    }
}
