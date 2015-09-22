namespace TestingContextCore.Implementation.Dependencies
{
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.ResolutionContext;

    internal interface IDependency
    {
        void Validate(ContextStore store);

        Definition DependsOn { get; }

        bool IsCollectionDependency { get; }

        bool DependsOnChild { get; }
    }

    internal interface IDependency<TSource> : IDependency
    {
        TSource GetValue(IResolutionContext parentContext);
    }
}
