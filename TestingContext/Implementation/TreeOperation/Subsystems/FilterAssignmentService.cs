namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Diag;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using static FilterProcessingService;
    using static NodeReorderingService;
    using static TestingContextCore.Implementation.Registrations.StoreExtension;

    internal static class FilterAssignmentService
    {
        public static INode GetAssignmentNode(Tree tree, IDepend depend)
        {
            var nodes = depend.Dependencies.Select(x => x.GetDependencyNode(tree)).ToList();
            var group = tree.GetParentGroup(depend);
            if (group != null)
            {
                nodes.Add(tree.GetNode(group.NodeToken));
            }

            return nodes.OrderByDescending(x => x.GetParentalChain().Count).First();
        }

        public static void AssignFilter(Tree tree, IFilter filter)
        {
            if (!filter.Dependencies.Any())
            {
                return;
            }
            
            var node = GetAssignmentNode(tree, filter);
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
            tree.GroupNodes.ForEach(AssignExistsFilter);
        }

        private static void AssignExistsFilter(INode node)
        {
            var info = new FilterInfo();
            var dependency = new CollectionDependency(node.Token);
            var filter = new ExistsFilter(dependency, info);
            AssignFilterToNode(filter, node.Parent);
        }
    }
}
