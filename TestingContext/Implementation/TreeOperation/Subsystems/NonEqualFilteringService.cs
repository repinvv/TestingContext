namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Registrations;
    using static FilterAssignmentService;

    internal static class NonEqualFilteringService
    {
        public static void AssignNonEqualFilters(Tree tree, IFilter filter, RegistrationStore store)
        {
            for (int i = 0; i < filter.Dependencies.Length; i++)
            {
                for (int j = i + 1; j < filter.Dependencies.Length; j++)
                {
                    var node1 = filter.Dependencies[i].GetDependencyNode(tree);
                    var node2 = filter.Dependencies[j].GetDependencyNode(tree);
                    if (node1.Definition.Type == node2.Definition.Type && node1.Definition.Key != node2.Definition.Key)
                    {
                        AssignFilter(tree, new NonEqualFilter(node1.Definition, node2.Definition), store);
                    }
                }
            }
        }
    }
}
