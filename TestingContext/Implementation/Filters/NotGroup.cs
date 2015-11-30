namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.PublicMembers.Exceptions;

    internal class NotGroup : BaseFilter, IFilterGroup
    {
        public NotGroup(IDiagInfo diagInfo, IFilter absorber) : base(diagInfo, absorber) { }

        public NotGroup(IFilter inner, IDiagInfo diagInfo, IFilter absorber = null) : base(diagInfo, absorber)
        {
            Filters.Add(inner);
        }

        public bool MeetsCondition(IResolutionContext context, out int[] failureWeight, out IFilter failure)
        {
            if (Filters.Count != 1)
            {
                throw new AlgorythmException("NOT group can only contain one filter");
            }

            failureWeight = FilterConstant.EmptyArray;
            failure = this;
            IFilter innerFailure;
            int[] innerWeight;
            return !Filters[0].MeetsCondition(context, out innerWeight, out innerFailure);
        }

        public List<IFilter> Filters { get; } = new List<IFilter>();

        public IEnumerable<IDependency> Dependencies => Filters.SelectMany(x => x.Dependencies);

        public IEnumerable<IToken> ForTokens => Dependencies.Select(x => x.Token);
    }
}
