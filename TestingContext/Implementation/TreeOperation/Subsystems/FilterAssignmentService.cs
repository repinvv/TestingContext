namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Linq;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.TreeOperation.Nodes;

    internal static class FilterAssignmentService
    {
        public static INode GetAssignmentNode(Tree tree, IHaveDependencies have)
        {
            return have.Dependencies
                       .Select(x => x.GetDependencyNode(tree))
                       .OrderByDescending(x => x.GetParentalChain().Count)
                       .First();
        }

        public static void AssignFilter(Tree tree, IFilter filter)
        {
            GetAssignmentNode(tree, filter).Filters.AddItemFilter(filter);
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
