namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;

    internal interface IFilterGroup : IFilter
    {
        List<IFilter> Filters { get; }
    }
}
