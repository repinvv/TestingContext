namespace TestingContextCore.Implementation.Tokens
{
    using System;
    using TestingContextLimitedInterface.Tokens;

    internal class Token<T> : IToken<T>
    {
        public string Name { get; set; }

        public Type Type => typeof(T);

        public override string ToString() => Type.Name + (string.IsNullOrEmpty(Name) ? string.Empty : (" \"" + Name + "\""));
    }
}
