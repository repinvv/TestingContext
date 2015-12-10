namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System;
    using TestingContext.Interface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.PublicMembers;
    using static NodeClosestParentService;
    using static NonEqualFilteringService;

    internal static class NodeReorderingService
    {
        public static void ReorderNodes(Tree tree, IDependency[] dependencies, IDiagInfo diagInfo)
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

                    ReorderNodes(tree, node1, node2, diagInfo);
                    AssignNonEqualFilter(tree, node1, node2);
                }
            }
        }

        private static void ReorderNodes(Tree tree, INode node1, INode node2, IDiagInfo diagInfo)
        {
            if (node1.IsChildOf(node2) || node2.IsChildOf(node1))
            {
                return;
            }

            var chain1 = node1.GetParentalChain();
            var chain2 = node2.GetParentalChain();
            var closestParentIndex = FindClosestParent(chain1, chain2);
            var firstChild1 = chain1[closestParentIndex + 1];
            var firstChild2 = chain2[closestParentIndex + 1];
            if (firstChild2.Index > firstChild1.Index)
            {
                firstChild2.Parent = node1;
                tree.ReorderedNodes.Add(new Tuple<INode, IDiagInfo>(firstChild2, diagInfo));
            }
            else
            {
                firstChild1.Parent = node2;
                tree.ReorderedNodes.Add(new Tuple<INode, IDiagInfo>(firstChild1, diagInfo));
            }
        }
    }
}
