namespace TestingContextCore.Implementation.Dependencies
{
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.ResolutionContext;

    internal interface IDependency
    {
        void Validate(ContextStore store);

        Definition DependsOn { get; }

        bool IsCollectionDependency { get; }
    }

    internal interface IDependency<TSource> : IDependency
        where TSource : class
    {
        TSource GetValue(IResolutionContext parentContext);
    }
}
