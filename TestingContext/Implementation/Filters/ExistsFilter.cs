namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.TreeOperation;

    internal class ExistsFilter : BaseFilter, IFilter
    {
        private readonly IDependency<IEnumerable<IResolutionContext>> dependency;

        public ExistsFilter(IDependency<IEnumerable<IResolutionContext>> dependency, IDiagInfo diagInfo)
            : base(diagInfo)
        {
            this.dependency = dependency;
            Dependencies = new[] { dependency };
            ForTokens = new[] { dependency.Token };
        }

        public IEnumerable<IDependency> Dependencies { get; }
        public IEnumerable<IToken> ForTokens { get; }

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            IEnumerable<IResolutionContext> argument;
            if (!dependency.TryGetValue(context, out argument) || !argument.Any())
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
