namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.PublicMembers;

    internal class Filter1<T1> : BaseFilter, IFilter
    {
        private readonly IDependency<T1> dependency;
        private readonly Func<T1, bool> filter;

        public Filter1(IDependency<T1> dependency, Func<T1, bool> filter, IDiagInfo diagInfo) : base(diagInfo)
        {
            this.dependency = dependency;
            this.filter = filter;
            Dependencies = new IDependency[] { dependency };
        }

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            T1 argument = dependency.GetValue(context);
            return filter(argument) ? null : this;
        }

        public IEnumerable<IDependency> Dependencies { get; }
        public IEnumerable<IToken> ForTokens => Dependencies.Select(x => x.Token);

        public override string ToString()
        {
            return $"filter for {Dependencies.First().Token}";
        }
    }
}
