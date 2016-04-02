namespace TestingContextCore.Implementation.Tokens
{
    using TestingContextLimitedInterface.Tokens;

    internal class FilterToken : IFilterToken
    {
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
