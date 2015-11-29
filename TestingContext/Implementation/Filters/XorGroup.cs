namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;
    using TestingContextCore.PublicMembers.Exceptions;

    internal class XorGroup : BaseFilter, IFilterGroup
    {
        public XorGroup(DiagInfo diagInfo, IFilter absorber) : base(diagInfo, absorber) { }

        public bool MeetsCondition(IResolutionContext context, out int[] failureWeight, out IFailure failure)
        {
            if (Filters.Count != 2)
            {
                throw new AlgorythmException("XOR group can only contain two filters");
            }

            failureWeight = FilterConstant.EmptyArray;
            failure = this;
            IFailure innerFailure;
            int[] innerWeight;
            return Filters[0].MeetsCondition(context, out innerWeight, out innerFailure) 
                ^ Filters[1].MeetsCondition(context, out innerWeight, out innerFailure);
        }

        public List<IFilter> Filters { get; } = new List<IFilter>();

        public IEnumerable<IDependency> Dependencies => Filters.SelectMany(x => x.Dependencies);

        public IEnumerable<IToken> ForTokens => Dependencies.Select(x => x.Token);
    }
}
