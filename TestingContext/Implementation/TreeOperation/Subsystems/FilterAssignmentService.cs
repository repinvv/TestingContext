namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.Interface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.PublicMembers;
    using static FilterProcessingService;
    using static NodeReorderingService;
    using static TestingContextCore.Implementation.Registrations.StoreExtension;

    internal static class FilterAssignmentService
    {
        public static INode GetAssignmentNode(Tree tree, IDepend have)
        {
            return have.Dependencies
                       .Select(x => x.GetDependencyNode(tree))
                       .OrderByDescending(x => x.GetParentalChain().Count)
                       .First();
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

        public static void AssignFilters(TokenStore store, Tree tree)
        {
            var freeFilters = new List<IFilter>();
            foreach (var filter in store.Filters)
            {
                ProcessFilterGroup(filter as IFilterGroup, freeFilters, store, tree);
                AddFilter(filter, freeFilters, store);
            }
            
            freeFilters.ForEach(x => ReorderNodes(tree, x.Dependencies.ToArray(), x.DiagInfo));
            freeFilters.ForEach(x => AssignFilter(tree, x));
            tree.ReorderedNodes.ForEach(x=>AssignExistsFilter(x.Item1, x.Item2));
        }

        private static void AssignExistsFilter(INode node, IDiagInfo diagInfo)
        {
            var filter = CreateExistsFilter(node.Token, diagInfo);
            AssignFilterToNode(filter, node.Parent);
        }
    }
}
