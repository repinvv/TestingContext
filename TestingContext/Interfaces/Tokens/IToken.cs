namespace TestingContextCore.Interfaces.Tokens
{
    using System;

    public interface IToken
    {
        string Name { get; }

        Type Type { get; }
    }

    public interface IToken<T> : IToken
    {
    }
}
