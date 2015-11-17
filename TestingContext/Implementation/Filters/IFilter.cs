namespace TestingContextCore.Implementation.Filters
{
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.ResolutionContext;

    internal interface IFilter : IHaveDependencies, IFailure
    {
        IFilterGroup Group { get; }

        bool MeetsCondition(IResolutionContext context, NodeResolver resolver, out int[] failureWeight, out IFailure failure);
    }
}
