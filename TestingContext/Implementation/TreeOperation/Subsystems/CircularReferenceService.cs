namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.TreeOperation.Nodes;
    using static NodeClosestParentService;

    internal static class CircularReferenceService
    {
        public static void DetectCircularReferences(Tree tree)
        {
            foreach (INode node in tree.Nodes.Values)
            {
                foreach (IDependency dependency in node.Filters.ItemFilter.Dependencies)
                {
                    DetectCircularReference(tree, node, dependency);
                }
            }
        }

        public static void DetectCircularReference(Tree tree, INode node1, IDependency dependency)
        {
            if (node1.Definition == dependency.Definition)
            {
                return;
            }

            var node2 = dependency.GetDependencyNode(tree);
            var chain1 = node1.GetParentalChain();
            var chain2 = node2.GetParentalChain();
            var closestParentIndex = FindClosestParent(chain1, chain2);
            var refIndex = closestParentIndex + 1;
            if (chain1.Count == refIndex || chain2.Count == refIndex)
            {
                return;
            }

            var branchRef = new BranchReference(chain1[closestParentIndex].Definition,
                                                chain1[refIndex].Definition,
                                                chain2[refIndex].Definition);
            if (tree.BranchReferences.Contains(branchRef.Backward))
            {
                throw new RegistrationException($"The reference between {node1} and {dependency.Definition} creates a circular dependency.");
            }

            tree.BranchReferences.Add(branchRef);
        }
    }
}
