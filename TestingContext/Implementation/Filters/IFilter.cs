namespace TestingContextCore.Implementation.Filters
{
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Implementation.TreeOperation.Nodes;

    internal interface IFilter : IFailure
    {
        IDependency[] Dependencies { get; }

        bool MeetsCondition(IResolutionContext context, NodeResolver resolver, out int[] failureWeight, out IFailure failure);
    }
}
