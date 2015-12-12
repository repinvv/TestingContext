namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;

    internal interface IFilterGroup : IFilter
    {
        IDependency[] GroupDependencies { get; }
        
        IToken GroupToken { get; }

        List<IFilter> Filters { get; }
    }
}
