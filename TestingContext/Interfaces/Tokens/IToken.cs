namespace TestingContextCore.Interfaces.Tokens
{
    using System;

    public interface IToken
    {
        string Name { get; set; }

        Type Type { get; }
    }
}
