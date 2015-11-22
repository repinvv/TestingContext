namespace TestingContextCore.Implementation.Dependencies
{
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces.Tokens;

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
