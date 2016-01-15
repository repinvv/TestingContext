namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;

    internal static class AbsorbedFiltersService
    {
        public static bool IsFilterAbsorbed(this TreeContext context, IFilter filter, IFilterGroup group)
        {
            var node = context.GetGroupNode(group);
            if (node == null)
            {
                return false;
            }

            return node.Children.Any(x => context.FilterIsAbsorbedBy(filter, x));
        }

        private static bool FilterIsAbsorbedBy(this TreeContext context, IFilter filter, INode node)
        {
            return filter.Dependencies.Any(x => context.DependencyIsAbsorbed(x, node));
        }

        private static bool DependencyIsAbsorbed(this TreeContext context, IDependency dependency, INode node)
        {
            if (dependency.Type == DependencyType.Single && dependency.Token == node.Token) return true;
            return context.IsParent(dependency.Token, node.Token);
        }
    }
}