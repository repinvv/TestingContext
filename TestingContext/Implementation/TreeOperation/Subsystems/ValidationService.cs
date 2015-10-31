namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Linq;
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.Filters;

    internal static class ValidationService
    {
        public static void ValidateFilter(Tree tree, IFilter filter)
        {
            for (int i = 0; i < filter.Dependencies.Length; i++)
            {
                var dep1 = filter.Dependencies[i];
                if (!dep1.IsCollectionDependency)
                {
                    continue;
                }

                for (int j = i + 1; j < filter.Dependencies.Length; j++)
                {
                    var dep2 = filter.Dependencies[j];
                    if (!dep2.IsCollectionDependency)
                    {
                        continue;
                    }

                    var node1 = tree.Nodes[dep1.Definition];
                    var node2 = tree.Nodes[dep2.Definition];
                    if (node1.Provider.Dependency.Definition != node2.Provider.Dependency.Definition)
                    {
                        throw new RegistrationException($"Filter {filter.Key} references two collections {node1} and" +
                                                        $"{node2}. Both of referenced collections should " +
                                                        $"branch off the same parent.");
                    }
                }
            }
        }

        public static void ValidateTree(Tree tree)
        {
            foreach (var relation in tree.ProhibitedRelations.Where(relation => relation.Child.IsChildOf(relation.Parent)))
            {
                throw new RegistrationException($"Filter {relation.Filter.Key} references parent collection.");
            }
        }
    }
}
