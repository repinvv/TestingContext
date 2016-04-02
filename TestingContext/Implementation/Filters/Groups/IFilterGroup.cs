namespace TestingContextCore.Implementation.Filters.Groups
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextLimitedInterface.Tokens;

    internal interface  IFilterGroup : IFilter
    {
        IDependency[] GroupDependencies { get; }
        
        IToken NodeToken { get; }

        List<IFilter> Filters { get; }
    }
}
