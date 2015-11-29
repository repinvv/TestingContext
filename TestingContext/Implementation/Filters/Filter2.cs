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

    internal class Filter2<T1, T2> : BaseFilter, IFilter
    {
        private readonly IDependency<T1> dependency1;
        private readonly IDependency<T2> dependency2;
        private readonly Func<T1, T2, bool> filter;

        public Filter2(IDependency<T1> dependency1,
            IDependency<T2> dependency2,
            Func<T1, T2, bool> filter, 
            DiagInfo diagInfo,
            IFilter absorber) 
            : base(diagInfo, absorber)
        {
            this.dependency1 = dependency1;
            this.dependency2 = dependency2;
            this.filter = filter;
            Dependencies = new IDependency[] { dependency1, dependency2 };
            ForTokens = new[] { dependency1.Token, dependency2.Token };
        }

        public bool MeetsCondition(IResolutionContext context, out int[] failureWeight, out IFailure failure)
        {
            T1 argument1;
            T2 argument2;
            failureWeight = FilterConstant.EmptyArray;
            failure = this;

            return dependency1.TryGetValue(context, out argument1) && dependency2.TryGetValue(context, out argument2) 
                && filter(argument1, argument2);
        }

        public IEnumerable<IDependency> Dependencies { get; }
        public IEnumerable<IToken> ForTokens { get; }

        public override string ToString()
        {
            return $"filter for {Dependencies.First() .Token} and {Dependencies.Last() .Token}";
        }
    }
}
