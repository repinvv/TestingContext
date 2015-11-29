namespace TestingContextCore.Implementation.Tokens
{
    using TestingContextCore.Interfaces.Tokens;

    internal class Token : IFilterToken
    {
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
