namespace TestingContextCore.Implementation.Filters.Groups
{
    using System.Collections.Generic;
    using global::TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;

    internal interface IFilterGroup : IFilter
    {
        IDependency[] GroupDependencies { get; }
        
        IToken GroupToken { get; }

        List<IFilter> Filters { get; }
    }
}
