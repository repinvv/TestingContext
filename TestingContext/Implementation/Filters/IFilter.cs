namespace TestingContextCore.Implementation.Filters
{
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.ResolutionContext;

    internal interface IFilter : IFailure
    {
        IDependency[] Dependencies { get; }

        bool MeetsCondition(IResolutionContext context);
    }
}
