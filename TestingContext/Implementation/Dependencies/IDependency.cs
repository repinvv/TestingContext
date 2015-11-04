namespace TestingContextCore.Implementation.Dependencies
{
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Implementation.TreeOperation.Nodes;

    internal interface IDependency
    {
        Definition Definition { get; }

        bool IsCollectionDependency { get; }
    }

    internal interface IDependency<TSource> : IDependency
    {
        bool TryGetValue(IResolutionContext context, out TSource value);
    }
}
