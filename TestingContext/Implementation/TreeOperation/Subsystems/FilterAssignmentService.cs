namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Linq;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Registrations;
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
                var node = tree.Nodes[filter.Dependencies[0].Definition];
                var parent = tree.Nodes[node.Provider.Dependency.Definition];
                parent.Filters.AddItemFilter(filter);
            }
        }

        public static void AssignCollectionValidityFilter(Tree tree, IFilter filter, RegistrationStore store)
        {
            var node = tree.Nodes[filter.Dependencies[0].Definition];
            if (store.CollectionInversions.Contains(node.Definition))
            {
                filter = new Inverter(filter);
            }

            node.Parent.Filters.AddItemFilter(filter);
        }
    }
}
