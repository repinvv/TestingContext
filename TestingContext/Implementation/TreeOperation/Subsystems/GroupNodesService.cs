namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Providers;
    using static GroupFiltersService;

    internal static class GroupNodesService
    {
        public static void CreateNodeForFilterGroup(IFilterGroup filterGroup, List<INode> nodes, Tree tree)
        {
            if (tree.GroupIsSameAsParent(filterGroup))
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
            nodes.Add(node);
        }

        private static Node CreateGroupNode(IFilterGroup filterGroup, Tree tree, HashSet<IDependency> groupDependencies)
        {
            var parentGroup = tree.GetParentGroup(filterGroup);
            var provider = new GroupProvider(groupDependencies, filterGroup, tree, filterGroup.DiagInfo);
            var node = Node.CreateNode(filterGroup.NodeToken, provider, tree);
            node.IsNegative = true;
            return node;
        }
    }
}
