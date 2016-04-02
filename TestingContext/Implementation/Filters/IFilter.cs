namespace TestingContextCore.Implementation.Filters
{
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextInterface;

    internal interface IFilter : IDepend, IFailure
    {
        FilterInfo FilterInfo { get; }

        IFilter GetFailingFilter(IResolutionContext context);
    }
}
