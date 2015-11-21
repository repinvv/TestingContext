namespace TestingContextCore.Implementation.Tokens
{
    using System;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Interfaces.Tokens;

    internal class Token : IToken
    {
        public string Name { get; set; }

        public Type Type => typeof(IFilter);

        public override string ToString() => Name;
    }
}
