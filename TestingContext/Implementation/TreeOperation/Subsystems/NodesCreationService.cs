namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Nodes;
    using static GroupNodesService;

    internal static class NodesCreationService
    {
        public static List<INode> CreateNodes(Tree tree)
        {
            List<INode> nodes = new List<INode>();
            foreach (var provider in tree.Store.Providers)
            {
                var node = Node.CreateNode(provider.Key, provider.Value, tree);
                node.IsNegative = provider.Value.IsNegative || tree.Store.CollectionInversions.ContainsKey(node.Token);
                nodes.Add(node);
            }
            
            tree.Filters.ForAllGroups(grp => CreateNodeForFilterGroup(grp, nodes, tree));
            return nodes;
        }
    }
}
