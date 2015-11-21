namespace TestingContextCore.OldImplementation.Dependencies
{
    using TestingContextCore.OldImplementation.ResolutionContext;

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
