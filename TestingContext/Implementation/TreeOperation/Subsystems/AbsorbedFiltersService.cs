namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;

    internal static class AbsorbedFiltersService
    {
        public static bool FilterIsAbsorbed(TreeContext context, List<IToken> inGroupTokens, IFilter filter)
        {
            return inGroupTokens.Where(x => filter. co != filter))
                            .Any(x => FilterIsAbsorbedBy(filter, tree.GetCvFilterNode(x), tree));
        }

        private static bool FilterIsAbsorbedBy(IFilter filter, INode cvNode, Tree tree)
        {
            return filter.Dependencies.Any(x => DependencyIsAbsorbed(x, cvNode, tree));
        }

        private static bool DependencyIsAbsorbed(IDependency dependency, INode cvNode, Tree tree)
        {
            if (dependency.Type == DependencyType.Single && dependency.Token == cvNode.Token) return true;
            return tree.IsParent(dependency.Token, cvNode.Token);
        }
    }
}