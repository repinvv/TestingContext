namespace TestingContextCore.Implementation.Dependencies
{
    using TestingContextCore.Implementation.ResolutionContext;

    internal interface IDependency
    {
        Definition Definition { get; }

        Definition ClosestParent { set; }

        bool IsCollectionDependency { get; }
    }

    internal interface IDependency<TSource> : IDependency
    {
        bool TryGetValue(IResolutionContext context, out TSource value);
    }
}
