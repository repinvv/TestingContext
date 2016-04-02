namespace TestingContextCore.Implementation.Dependencies
{
    using System;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextLimitedInterface.Tokens;

    internal interface IDependency
    {
        IToken Token { get; }

        DependencyType Type { get; }

        Type SourceType { get; }
    }

    internal interface IDependency<TSource> : IDependency
    {
        TSource GetValue(IResolutionContext context);
    }
}
