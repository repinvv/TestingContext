namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.TreeOperation.Nodes;
    using static NodeClosestParentService;

    internal class NodeReorderingService
    {
        public static void ReorderNodesForFilter(Tree tree, IFilter filter)
        {
            for (int i = 0; i < filter.Dependencies.Length; i++)
            {
                for (int j = i + 1; j < filter.Dependencies.Length; j++)
                {
                    ReorderNodesForDependencies(tree, filter.Dependencies[i], filter.Dependencies[j]);
                }
            }
        }

        private static void ReorderNodesForDependencies(Tree tree, IDependency dep1, IDependency dep2)
        {
            if (dep1.Definition == dep2.Definition || dep1.IsCollectionDependency || dep2.IsCollectionDependency)
            {
                return;
            }

            var node1 = tree.Nodes[dep1.Definition];
            var node2 = tree.Nodes[dep2.Definition];
            if (node1.IsChildOf(node2) || node2.IsChildOf(node1))
            {
                return;
            }

            var chain1 = node1.GetParentalChain();
            var chain2 = node2.GetParentalChain();
            var closestParentIndex = FindClosestParent(chain1, chain2);
            chain1[closestParentIndex + 1].Parent = node2;
        }
    }
}
