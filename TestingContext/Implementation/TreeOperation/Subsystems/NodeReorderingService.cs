﻿namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.PublicMembers.Exceptions;
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
            ValidateReordering(chain1, chain2, closestParentIndex, diagInfo);
            var firstChild1 = chain1[closestParentIndex + 1];
            var firstChild2 = chain2[closestParentIndex + 1];
            if (firstChild2.Id > firstChild1.Id)
            {
                firstChild2.Parent = node1;
                tree.NodesToCreateExistsFilter.Add(new Tuple<INode, IDiagInfo>(firstChild2, diagInfo));
            }
            else
            {
                firstChild1.Parent = node2;
                tree.NodesToCreateExistsFilter.Add(new Tuple<INode, IDiagInfo>(firstChild1, diagInfo));
            }
        }

        private static void ValidateReordering(List<INode> chain1, List<INode> chain2, int closestParentIndex, IDiagInfo diagInfo)
        {
            var invalidNode1 = GetInvalidNode(chain1, closestParentIndex);
            var invalidNode2 = GetInvalidNode(chain2, closestParentIndex);
            if (invalidNode1 == null && invalidNode2 == null)
            {
                return;
            }

            var fixNode1 = invalidNode1 ?? chain1.Last();
            var fixNode2 = invalidNode2 ?? chain2.Last();

            var exceptionMessage = $"There is an uncertainty between {chain1.Last()} (a) and {chain2.Last()} (b)" + Environment.NewLine+
                $"{GetInvalidateString("a", chain1.Last(), invalidNode1)}{GetInvalidateString("b", chain2.Last(), invalidNode2)}" +
                $"To deal with this uncertainty, you can either make {fixNode1} depend on {chain2.Last()} or {fixNode2} depend on {chain1.Last()}";
            var tuples = new List<Tuple<IToken, IDiagInfo>>
                         {
                             GetNodeTuple(fixNode1),
                             GetNodeTuple(chain1.Last()),
                             GetNodeTuple(fixNode2),
                             GetNodeTuple(chain2.Last()),
                         };

            throw new DetailedRegistrationException(exceptionMessage, tuples, diagInfo);
        }

        private static Tuple<IToken, IDiagInfo> GetNodeTuple(INode node)
        {
            return new Tuple<IToken, IDiagInfo>(node.Token, node.Provider.DiagInfo);
        }

        private static string GetInvalidateString(string marker, INode last, INode invalid)
        {
            if (invalid == null)
            {
                return string.Empty;
            }

            if (invalid == last)
            {
                return $"({marker}) is negated, i.e. DoesNotExist or Each condition" + Environment.NewLine;
            }

            if (invalid.Token is GroupToken)
            {
                return $"({marker}) is under {invalid}" + Environment.NewLine;
            }
            
            return $"({marker}) is under node {invalid}, which is either DoesNotExist or Each" + Environment.NewLine;
        }

        private static INode GetInvalidNode(List<INode> chain, int closestParentIndex)
        {
            for (int i = chain.Count - 1; i > closestParentIndex; i--)
            {
                if (chain[i].IsNegative)
                {
                    return chain[i];
                }
            }

            return null;
        }
    }
}
