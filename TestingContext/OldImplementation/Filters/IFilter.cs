namespace TestingContextCore.OldImplementation.Filters
{
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.FailureInfo;
    using TestingContextCore.OldImplementation.Nodes;
    using TestingContextCore.OldImplementation.ResolutionContext;

    internal interface IFilter : IHaveDependencies, IFailure
    {
        IFilterGroup Group { get; }

        bool MeetsCondition(IResolutionContext context, NodeResolver resolver, out int[] failureWeight, out IFailure failure);
    }
}
