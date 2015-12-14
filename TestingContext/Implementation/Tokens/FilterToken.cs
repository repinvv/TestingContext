namespace TestingContextCore.Implementation.Tokens
{
    using global::TestingContext.LimitedInterface.Tokens;

    internal class FilterToken : IFilterToken
    {
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
