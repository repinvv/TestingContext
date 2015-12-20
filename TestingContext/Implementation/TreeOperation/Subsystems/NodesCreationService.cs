namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using static GroupNodesService;

    internal static class NodesCreationService
    {
        public static Dictionary<IToken, List<INode>> GetNodesWithDependencies(TokenStore store, Tree tree)
        {
            List<Node> nodes = new List<Node>();
            foreach (var provider in store.Providers)
            {
                var node = Node.CreateNode(provider.Key, provider.Value, store, tree);
                node.IsNegative = provider.Value.IsNegative || store.CollectionInversions.ContainsKey(node.Token);
                tree.Nodes.Add(node.Token, node);
                nodes.Add(node);
            }

            var nodeDependencies = GroupNodes(nodes);
            tree.Filters.ForGroups(grp=> CreateNodeForFilterGroup(grp, nodeDependencies, store, tree));
            return nodeDependencies;
        }

        private static Dictionary<IToken, List<INode>> GroupNodes(List<Node> nodes)
        {
            var dict = new Dictionary<IToken, List<INode>>();
            foreach (var node in nodes)
            {
                foreach (var dependency in node.Provider.Dependencies)
                {
                    dict.GetList(dependency.Token).Add(node);
                }
            }

            return dict;
        }
    }
}
