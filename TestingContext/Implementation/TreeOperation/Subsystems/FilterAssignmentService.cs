namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using static NodeReorderingService;

    internal static class FilterAssignmentService
    {
        public static void AssignFilter(Tree tree, IFilter filter)
        {
            if (!filter.Dependencies.Any())
            {
                return;
            }
            
            var node = tree.GetAssignmentNode(filter);
            AssignFilterToNode(filter, node);
        }

        public static void AssignFilterToNode(IFilter filter, INode node)
        {
            node.FilterInfo.Group.Filters.Add(filter);
            var newIndex = node.Tree.FilterIndex.Any() ? (node.Tree.FilterIndex.Values.Max() + 1) : 0;
            node.Tree.FilterIndex[filter] = newIndex;
        }

        public static void AssignFilters(Tree tree)
        {
            tree.Filters.ForEach(x => x.ForDependencies((dep1, dep2) => ReorderNodes(x, tree, dep1, dep2)));
            tree.Filters.ForEach(x => AssignFilter(tree, x));
        }
    }
}
