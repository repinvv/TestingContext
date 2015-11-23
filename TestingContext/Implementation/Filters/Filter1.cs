namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    internal class Filter1<T1> : IFilter
    {
        private readonly IDependency<T1> dependency;
        private readonly Func<T1, bool> filter;
        private readonly IFilter absorber;

        public Filter1(IDependency<T1> dependency, Func<T1, bool> filter, DiagInfo diagInfo, IFilter absorber)
        {
            this.dependency = dependency;
            this.filter = filter;
            this.absorber = absorber;
            Dependencies = new IDependency[] { dependency };
            ForTokens = new[] { dependency.Token };
            DiagInfo = diagInfo;
        }

        public bool MeetsCondition(IResolutionContext context, out int[] failureWeight, out IFailure failure)
        {
            T1 argument;
            failureWeight = FilterConstant.EmptyArray;
            failure = absorber ?? this;
            if (!dependency.TryGetValue(context, out argument))
            {
                return false;
            }

            return filter(argument);
        }

        public IEnumerable<IDependency> Dependencies { get; }

        #region IFailure
        public IEnumerable<IToken> ForTokens { get; }
        public IFilterToken Token { get; } = new Token();
        public DiagInfo DiagInfo { get; }
        #endregion
    }
}
