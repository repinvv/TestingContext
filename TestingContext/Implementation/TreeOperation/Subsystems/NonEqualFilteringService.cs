namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Registrations;
    using static FilterAssignmentService;

    internal static class NonEqualFilteringService
    {
        public static void AssignNonEqualFilters(Tree tree, IHaveDependencies have, RegistrationStore store)
        {
            for (int i = 0; i < have.Dependencies.Length; i++)
            {
                for (int j = i + 1; j < have.Dependencies.Length; j++)
                {
                    var node1 = have.Dependencies[i].GetDependencyNode(tree);
                    var node2 = have.Dependencies[j].GetDependencyNode(tree);
                    if (node1.Definition.Type != node2.Definition.Type || node1.Definition.Key == node2.Definition.Key)
                    {
                        continue;
                    }

                    var tuple = new Tuple<Definition, Definition>(node1.Definition, node2.Definition);
                    var reverseTuple = new Tuple<Definition, Definition>(node2.Definition, node1.Definition);
                    if (tree.NonEqualFilters.Contains(tuple) || tree.NonEqualFilters.Contains(reverseTuple))
                    {
                        continue;
                    }

                    tree.NonEqualFilters.Add(tuple);
                    AssignFilter(tree, new NonEqualFilter(node1.Definition, node2.Definition), store);
                }
            }
        }
    }
}
