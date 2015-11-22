namespace TestingContextCore.Implementation.Filters
{
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;

    internal interface IFilter : IHaveDependencies, IFailure
    {
        bool MeetsCondition(IResolutionContext context, out int[] failureWeight, out IFailure failure);
    }
}
