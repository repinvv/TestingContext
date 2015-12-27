namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Nodes;
    using static GroupNodesService;

    internal static class NodesCreationService
    {
        public static List<INode> CreateNodes(TreeContext context)
        {
            List<INode> nodes = new List<INode>();
            foreach (var provider in context.Store.Providers)
            {
                var node = Node.CreateNode(provider.Key, provider.Value, context);
                node.IsNegative = provider.Value.IsNegative || context.Store.CollectionInversions.ContainsKey(node.Token);
                nodes.Add(node);
            }
            
            context.Filters.ForGroups(grp => CreateNodeForFilterGroup(grp, nodes, context));
            return nodes;
        }
    }
}
