namespace TestingContextCore.Implementation.Dependencies
{
    using System;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Resolution;

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
