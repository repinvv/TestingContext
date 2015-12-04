namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using static NodeClosestParentService;
    using static NonEqualFilteringService;

    internal static class NodeReorderingService
    {
        public static void ReorderNodes(Tree tree, IDependency[] dependencies, IFilter absorber)
        {
            for (int i = 0; i < dependencies.Length; i++)
            {
                for (int j = i + 1; j < dependencies.Length; j++)
                {
                    var node1 = dependencies[i].GetDependencyNode(tree);
                    var node2 = dependencies[j].GetDependencyNode(tree);
                    if (node1 == node2)
                    {
                        continue;
                    }

                    ReorderNodes(tree, node1, node2, absorber);
                    AssignNonEqualFilter(tree, node1, node2);
                }
            }
        }

        private static void ReorderNodes(Tree tree, INode node1, INode node2, IFilter absorber)
        {
            if (node1.IsChildOf(node2) || node2.IsChildOf(node1))
            {
                return;
            }

            var chain1 = node1.GetParentalChain();
            var chain2 = node2.GetParentalChain();
            var closestParentIndex = FindClosestParent(chain1, chain2);
            var reorderedNode = chain2[closestParentIndex + 1];
            reorderedNode.Parent = node1;
            tree.ReorderedNodes.Add(new Tuple<INode, IFilter>(reorderedNode, absorber));
        }
    }
}
