namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;

    internal class ExistsFilter : BaseFilter, IFilter
    {
        private readonly IDependency<IEnumerable<IResolutionContext>> dependency;

        public ExistsFilter(IDependency<IEnumerable<IResolutionContext>> dependency, FilterInfo info)
            : base(info)
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
                           .OrderByDescending(x => context.Node.Tree.FilterIndex.SafeGet(x))
                           .First();
        }

        public override string ToString()
        {
            return $"ExistsFilter for {ForTokens.First()}, Id: {FilterInfo.Id}";
        }
    }
}