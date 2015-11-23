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

    internal class Filter2<T1, T2> : IFilter
    {
        private readonly IDependency<T1> dependency1;
        private readonly IDependency<T2> dependency2;
        private readonly Func<T1, T2, bool> filter;
        private readonly IFilter absorber;

        public Filter2(IDependency<T1> dependency1,
            IDependency<T2> dependency2,
            Func<T1, T2, bool> filter, 
            DiagInfo diagInfo,
            IFilter absorber)
        {
            this.dependency1 = dependency1;
            this.dependency2 = dependency2;
            this.filter = filter;
            this.absorber = absorber;
            Dependencies = new IDependency[] { dependency1, dependency2 };
            ForTokens = new[] { dependency1.Token, dependency2.Token };
            DiagInfo = diagInfo;
        }

        #region IFilter
        public bool MeetsCondition(IResolutionContext context, out int[] failureWeight, out IFailure failure)
        {
            T1 argument1;
            T2 argument2;
            failureWeight = FilterConstant.EmptyArray;
            failure = absorber ?? this;

            if (!dependency1.TryGetValue(context, out argument1) || !dependency2.TryGetValue(context, out argument2))
            {
                return false;
            }

            return filter(argument1, argument2);
        }

        public IEnumerable<IDependency> Dependencies { get; }
        #endregion

        #region IFailure
        public IEnumerable<IToken> ForTokens { get; }
        public IFilterToken Token { get; } = new Token();
        public DiagInfo DiagInfo { get; }
        #endregion
    }
}
