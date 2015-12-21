namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Registrations;
    using static GroupFiltersService;

    internal static class GroupNodesService
    {
        public static void CreateNodeForFilterGroup(IFilterGroup filterGroup,
            Dictionary<IToken, List<INode>> nodeDependencies,
            Tree tree)
        {
            if (filterGroup.NodeToken == tree.GetParentGroup(filterGroup)?.NodeToken)
            {
                // in case where AndGroup is created inside OrGroup or XorGroup, 
                //it gets the token of parent group and does not get its own node
                return;
            }

            var inGroupTokens = new HashSet<IToken>(GetInGroupTokens(filterGroup, tree));
            if (!inGroupTokens.Any())
            {
                // if there are no declarations in the group, only filters, no node is needed
                return;
            }

            var groupDependencies = GetGroupDependencies(filterGroup, inGroupTokens, tree);
            var node = CreateGroupNode(filterGroup, tree, groupDependencies);
            AddNodeToDependOnOtherNodes(node, groupDependencies, nodeDependencies);
            AddInGroupNodesToDependOnNode(filterGroup, inGroupTokens, nodeDependencies, tree);
        }

        private static void AddInGroupNodesToDependOnNode(IFilterGroup filterGroup, 
            HashSet<IToken> inGroupTokens, 
            Dictionary<IToken, List<INode>> nodeDependencies, 
            Tree tree)
        {
            // all in group nodes to depend on created node
            var ingroupNodes = inGroupTokens.Select(inGroupToken => tree.Nodes[inGroupToken]);
            nodeDependencies.GetList(filterGroup.NodeToken).AddRange(ingroupNodes);
        }

        private static void AddNodeToDependOnOtherNodes(Node node, 
            HashSet<IDependency> groupDependencies, 
            Dictionary<IToken, List<INode>> nodeDependencies)
        {
            // add node to depend on other nodes, as per group dependencies
            foreach (var groupDependency in groupDependencies.Select(x => x.Token).Distinct())
            {
                nodeDependencies.GetList(groupDependency).Add(node);
            }
        }

        private static Node CreateGroupNode(IFilterGroup filterGroup, Tree tree, HashSet<IDependency> groupDependencies)
        {
            var parentGroup = tree.GetParentGroup(filterGroup);
            var provider = new GroupProvider(groupDependencies, parentGroup, tree.Store, filterGroup.DiagInfo);
            var node = Node.CreateNode(filterGroup.NodeToken, provider, tree);
            node.IsNegative = true;
            tree.Nodes.Add(node.Token, node);
            tree.GroupNodes.Add(node);
            return node;
        }
    }
}
