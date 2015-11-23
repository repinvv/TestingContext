namespace TestingContextCore.Implementation.Tokens
{
    using System;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Interfaces.Tokens;

    internal class Token : IFilterToken
    {
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
