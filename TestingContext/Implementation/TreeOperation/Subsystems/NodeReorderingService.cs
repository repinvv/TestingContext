namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Nodes;
    using static NodeClosestParentService;
    using static NonEqualFilteringService;

    internal static class NodeReorderingService
    {
        public static void ReorderNodes(Tree tree, IHaveDependencies have)
        {
            var dependencies = have.Dependencies.ToArray();
            for (int i = 0; i < dependencies.Length; i++)
            {
                for (int j = i + 1; j < dependencies.Length; j++)
                {
                    var node1 = dependencies[i].GetDependencyNode(tree);
                    var node2 = dependencies[j].GetDependencyNode(tree);
                    ReorderNodes(tree, node1, node2);
                }
            }
        }

        private static void ReorderNodes(Tree tree, INode node1, INode node2)
        {
            if (node1 != node2 && !node1.IsChildOf(node2) && !node2.IsChildOf(node1))
            {
                var chain1 = node1.GetParentalChain();
                var chain2 = node2.GetParentalChain();
                var closestParentIndex = FindClosestParent(chain1, chain2);
                chain2[closestParentIndex + 1].Parent = node1;
            }

            AssignNonEqualFilter(tree, node1, node2);
        }
    }
}
