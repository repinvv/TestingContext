namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    internal class Filter1<T1> : BaseFilter, IFilter
    {
        private readonly IDependency<T1> dependency;
        private readonly Func<T1, bool> filter;

        public Filter1(IDependency<T1> dependency, Func<T1, bool> filter, DiagInfo diagInfo, IFilter absorber) : base(diagInfo, absorber)
        {
            this.dependency = dependency;
            this.filter = filter;
            Dependencies = new IDependency[] { dependency };
            ForTokens = new[] { dependency.Token };
        }

        public bool MeetsCondition(IResolutionContext context, out int[] failureWeight, out IFailure failure)
        {
            T1 argument;
            failureWeight = FilterConstant.EmptyArray;
            failure = this;
            if (!dependency.TryGetValue(context, out argument))
            {
                return false;
            }

            return filter(argument);
        }

        public IEnumerable<IDependency> Dependencies { get; }
        public IEnumerable<IToken> ForTokens { get; }

        public override string ToString()
        {
            return $"filter for {Dependencies.First().Token}";
        }
    }
}
