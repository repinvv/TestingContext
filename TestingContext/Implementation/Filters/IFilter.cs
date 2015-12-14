namespace TestingContextCore.Implementation.Filters
{
    using global::TestingContext.Interface;
    using global::TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;

    internal interface IFilter : IDepend, IFailure
    {
        int Id { get; set; }

        IFilterToken Token { get; }

        IFilter GetFailingFilter(IResolutionContext context);
    }
}
