namespace TestingContextCore.Implementation.Dependencies
{
    using TestingContextCore.Implementation.ResolutionContext;

    internal interface IDependency
    {
        Definition Definition { get; }

        DependencyType Type { get; }
    }

    internal interface IDependency<TSource> : IDependency
    {
        bool TryGetValue(IResolutionContext context, out TSource value);
    }
}
