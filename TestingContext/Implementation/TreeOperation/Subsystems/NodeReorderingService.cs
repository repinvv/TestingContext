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
                    if (!ReorderNodesForDependencies(tree, filter.Dependencies[i], filter.Dependencies[j]))
                    {
                        throw new ResolutionException($"Filter implies chaining of entities, that is not allowed by other filters. " +
                                                      $"This filter makes graph that was not successfully resolved." +
                                                      $"{filter.Key} {filter.FilterString} ");
                    }
                }
            }
        }

        private static bool ReorderNodesForDependencies(Tree tree, IDependency dep1, IDependency dep2)
        {
            if (dep1.Definition == dep2.Definition || dep1.IsCollectionDependency || dep2.IsCollectionDependency)
            {
                return true;
            }

            var node1 = tree.Nodes[dep1.Definition];
            var node2 = tree.Nodes[dep2.Definition];
            if (node1.IsChildOf(node2) || node2.IsChildOf(node1))
            {
                return true;
            }

            var chain1 = node1.GetParentalChain();
            var chain2 = node2.GetParentalChain();
            var closestParentIndex = FindClosestParent(chain1, chain2);
            var chained = ChainUpNodes(tree, chain1[closestParentIndex + 1], node2);
            chained = chained || ChainUpNodes(tree, chain1[closestParentIndex + 1], node2);
            return chained;
        }

        private static bool ChainUpNodes(Tree tree, INode node, INode newParent)
        {
            if (!ChainUpIsValid(tree, node, newParent))
            {
                return false;
            }

            node.Parent = newParent;
            return true;
        }

        private static bool ChainUpIsValid(Tree tree, INode node, INode newParent)
        {
            return !tree.ProhibitedRelations
                        .Where(prohibitedRelation => prohibitedRelation.Child.IsChildOf(node))
                        .Any(prohibitedRelation => newParent.IsChildOf(prohibitedRelation.Parent));
        }
    }
}
