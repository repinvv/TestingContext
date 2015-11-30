namespace TestingContextCore.Implementation.Tokens
{
    using TestingContext.LimitedInterface;

    internal class Token : IFilterToken
    {
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
