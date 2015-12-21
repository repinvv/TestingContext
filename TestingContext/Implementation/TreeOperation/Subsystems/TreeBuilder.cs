namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.PublicMembers.Exceptions;
    using static FilterAssignmentService;
    using static NodeReorderingService;

    internal static class TreeBuilder
    {
        private static void AssignNode(INode node, Tree tree)
        {
            node.Provider.ForDependencies((dep1, dep2) => ReorderNodes(node.Provider, tree, dep1, dep2));
            var parent = GetAssignmentNode(tree, node.Provider);
            node.Parent = parent;
            node.SourceParent = parent;
        }

        public static void BuildNodesTree(Tree tree, Dictionary<IToken, List<INode>> nodeDependencies)
        {
            var assigned = nodeDependencies.ForNodes(tree, node => AssignNode(node, tree));

            foreach (var node in nodeDependencies.SelectMany(x => x.Value).Distinct().Where(node => !assigned.Contains(node.Token)))
            {
                throw new RegistrationException($"Could not put {node} to the resolution tree, please check registrations.", node.Provider.DiagInfo);
            }
        }
    }
}
