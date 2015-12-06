namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;

    internal class ExistsFilter : BaseFilter, IFilter
    {
        private readonly IDependency<IEnumerable<IResolutionContext>> dependency;

        public ExistsFilter(IDependency<IEnumerable<IResolutionContext>> dependency, IDiagInfo diagInfo)
            : base(diagInfo)
        {
            this.dependency = dependency;
            Dependencies = new[] { dependency };
        }

        public IEnumerable<IDependency> Dependencies { get; }
        public IEnumerable<IToken> ForTokens => Dependencies.Select(x => x.Token);

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            IEnumerable<IResolutionContext> argument = dependency.GetValue(context);

            if (!argument.Any())
            {
                return this;
            }

            if (argument.Any(x => x.MeetsConditions))
            {
                return null;
            }

            return argument.Select(x => x.FailingFilter)
                           .OrderByDescending(x => context.Node.Tree.FilterIndex[x])
                           .First();
        }
    }
}