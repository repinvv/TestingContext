namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::TestingContext.LimitedInterface.Diag;
    using global::TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.PublicMembers.Exceptions;

    internal class Filter2<T1, T2> : BaseFilter, IFilter
    {
        private readonly IDependency<T1> dependency1;
        private readonly IDependency<T2> dependency2;
        private readonly Func<T1, T2, bool> filter;

        public Filter2(IDependency<T1> dependency1,
            IDependency<T2> dependency2,
            Func<T1, T2, bool> filter,
            IFilterGroup group, 
            IDiagInfo diagInfo) 
            : base(group, diagInfo)
        {
            this.dependency1 = dependency1;
            this.dependency2 = dependency2;
            this.filter = filter;
            Dependencies = new IDependency[] { dependency1, dependency2 };
        }

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            T1 argument1 = dependency1.GetValue(context);
            T2 argument2 = dependency2.GetValue(context);
            try
            {
                return filter(argument1, argument2) ? null : this;
            }
            catch (Exception ex)
            {
                throw new RegistrationException($"Exception in registered expression for a filter", DiagInfo, ex);
            }
        }

        public IEnumerable<IDependency> Dependencies { get; }
        public IEnumerable<IToken> ForTokens => Dependencies.Select(x => x.Token);

        public override string ToString()
        {
            return $"filter for {Dependencies.First() .Token} and {Dependencies.Last() .Token}, Id: {Id}";
        }
    }
}
