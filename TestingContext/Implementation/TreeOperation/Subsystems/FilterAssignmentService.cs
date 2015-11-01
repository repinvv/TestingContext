namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Linq;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.TreeOperation.Nodes;

    internal static class FilterAssignmentService
    {
        public static void AssignFilter(Tree tree, IFilter filter)
        {
            var singular = filter.Dependencies.Where(x => !x.IsCollectionDependency).ToList();
            INode assignment = null;
            foreach (var dependency in singular)
            {
                var node = tree.Nodes[dependency.Definition];
                if (assignment == null || node.IsChildOf(assignment))
                {
                    assignment = node;
                }
            }

            if (assignment != null)
            {
                assignment.Filters.AddItemFilter(filter);
            }
            else
            {
                var dependency = filter.Dependencies.First(x => x.IsCollectionDependency);
                tree.Nodes[dependency.Definition].Filters.AddCollectionFilter(filter);
            }
        }

        public static void AssignCollectionFiltersToParents(Tree tree)
        {
            foreach (var node in tree.Nodes.Values.Where(x => x.Provider != null))
            {
                var parent = tree.Nodes[node.Provider.Dependency.Definition];
                parent.Filters.AddItemFilter(node.Filters.CollectionFilter);
            }
        }
    }
}
