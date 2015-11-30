namespace TestingContextCore.Implementation.Filters
{
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;

    internal interface IFilter : IHaveDependencies, IFailure
    {
        IFilterToken Token { get; }

        IFilter Absorber { get; }

        bool MeetsCondition(IResolutionContext context, out int[] failureWeight, out IFilter failure);
    }
}
