namespace TestingContextCore.Implementation.Dependencies
{
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Resolution;

    internal interface IDependency
    {
        IToken Token { get; }

        DependencyType Type { get; }
    }

    internal interface IDependency<TSource> : IDependency
    {
        bool TryGetValue(IResolutionContext context, out TSource value);
    }
}
