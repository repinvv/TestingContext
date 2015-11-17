namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using TestingContextCore.Implementation.Nodes;
    using static NodeClosestParentService;

    internal static class NodeReorderingService
    {
        public static void ReorderNodes(Tree tree, IHaveDependencies have)
        {
            for (int i = 0; i < have.Dependencies.Length; i++)
            {
                for (int j = i + 1; j < have.Dependencies.Length; j++)
                {
                    var node1 = have.Dependencies[i].GetDependencyNode(tree);
                    var node2 = have.Dependencies[j].GetDependencyNode(tree);
                    TryReorderNodes(node1, node2);
                }
            }
        }

        private static void TryReorderNodes(INode node1, INode node2)
        {
            if (node1 == node2 || node1.IsChildOf(node2) || node2.IsChildOf(node1))
            {
                return;
            }

            var chain1 = node1.GetParentalChain();
            var chain2 = node2.GetParentalChain();
            var closestParentIndex = FindClosestParent(chain1, chain2);
            chain2[closestParentIndex + 1].Parent = node1;
        }
    }
}
