namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.Interface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using static NodeClosestParentService;
    using static NonEqualFilteringService;

    internal static class NodeReorderingService
    {
        public static void ReorderNodes(Tree tree, IDepend depend)
        {
            var dependencies = depend.Dependencies.ToArray();
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

                    ReorderNodes(tree, node1, node2, depend.DiagInfo);
                    AssignNonEqualFilter(tree, node1, node2, depend.Group, depend.DiagInfo);
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
            ValidateReordering(chain1, chain2, closestParentIndex);
            var firstChild2 = chain2[closestParentIndex + 1];
            firstChild2.Parent = node1;
            tree.NodesToCreateExistsFilter.Add(new Tuple<INode, IDiagInfo>(firstChild2, diagInfo));
        }

        private static void ValidateReordering(List<INode> chain1, List<INode> chain2, int closestParentIndex)
        {
            
        }
    }
}
