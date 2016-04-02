namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.TreeOperation.Subsystems.NodeRelated;
    using TestingContextCore.PublicMembers.Exceptions;
    using TestingContextLimitedInterface.Tokens;

    internal static class TreeBuilder
    {
        private static void AssignNode(INode node, TreeContext context)
        {
            node.Provider.ForDependencies((dep1, dep2) => context.ReorderNodes(node.Provider, dep1, dep2));
            var parent = context.GetAssignmentNode(node.Provider);
            node.Parent = parent;
            node.SourceParent = parent;
        }

        public static void BuildNodesTree(this TreeContext context, Dictionary<IToken, List<INode>> nodeDependencies)
        {
            nodeDependencies.ForNodes(context, node => AssignNode(node, context));
            foreach (var node in context.Tree.Nodes.Values
                                        .Where(x => x != context.Tree.Root)
                                        .Where(x => x.SourceParent == null))
            {
                throw new RegistrationException($"Could not put {node} to the resolution tree, please check registrations.", node.Provider.DiagInfo);
            }
        }
    }
}
