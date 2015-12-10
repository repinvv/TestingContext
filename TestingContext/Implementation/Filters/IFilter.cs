namespace TestingContextCore.Implementation.Filters
{
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;

    internal interface IFilter : IDepend, IFailure
    {
        int Id { get; set; }

        IFilterToken Token { get; }

        IFilter GetFailingFilter(IResolutionContext context);
    }
}
