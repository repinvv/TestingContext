namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.PublicMembers;

    internal class Filter1<T1> : BaseFilter, IFilter
    {
        private readonly IDependency<T1> dependency;
        private readonly Func<T1, bool> filter;

        public Filter1(IDependency<T1> dependency, Func<T1, bool> filter, DiagInfo diagInfo) : base(diagInfo)
        {
            this.dependency = dependency;
            this.filter = filter;
            Dependencies = new IDependency[] { dependency };
            ForTokens = new[] { dependency.Token };
        }

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            T1 argument;
            if (dependency.TryGetValue(context, out argument) && filter(argument))
            {
                return null;
            }

            return this;
        }

        public IEnumerable<IDependency> Dependencies { get; }
        public IEnumerable<IToken> ForTokens { get; }

        public override string ToString()
        {
            return $"filter for {Dependencies.First().Token}";
        }
    }
}
