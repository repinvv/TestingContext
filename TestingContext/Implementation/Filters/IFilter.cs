namespace TestingContextCore.Implementation.Filters
{
    using System;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;

    internal interface IFilter : IHaveDependencies, IFailure
    {
        IFilterToken Token { get; }

        IFilter GetFailingFilter(IResolutionContext context);
    }
}
