namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using TestingContext.LimitedInterface;

    internal interface IFilterGroup : IFilter
    {
        IToken GroupToken { get; }

        List<IFilter> Filters { get; }
    }
}
