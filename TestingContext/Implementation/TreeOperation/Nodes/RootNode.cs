namespace TestingContextCore.Implementation.TreeOperation.Nodes
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Providers;

    internal class RootNode : INode
    {
        public RootNode(Tree tree, Definition definition)
        {
            Definition = definition;
            Resolver = new NodeResolver(tree, definition);
        }

        public Definition Definition { get; }

        public List<INode> Children { get; } = new List<INode>();

        public INode Parent { get; set; }

        public NodeFilters Filters { get; } = new NodeFilters();

        public NodeResolver Resolver { get; }

        public IProvider Provider => null;

        public bool IsChildOf(INode node) => false;

        public List<INode> GetNodesChain() => new List<INode> { this };
    }
}
