namespace TestingContextLimitedInterface.Tokens
{
    using System;

    public interface IToken<T> : IToken { }

    public interface IToken
    {
        string Name { get; set; }

        Type Type { get; }
    }
}
