namespace TestingContextCore.Implementation.Filters
{
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal interface IFilter : IHaveDependencies, IFailure
    {
        IFilterGroup Group { get; }

        bool MeetsCondition(IResolutionContext context, out int[] failureWeight, out IFailure failure);
    }
}
