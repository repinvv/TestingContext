namespace TestingContextCore.Implementation.Nodes
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.TreeOperation;

    internal class RootNode : INode
    {
        public RootNode(Tree tree, Definition definition)
        {
            Definition = definition;
            Resolver = new NodeResolver(tree, definition);
        }

        public Definition Definition { get; }

        public INode SourceParent { get; set; }

        public INode Parent { get; set; }

        public NodeFilters Filters { get; } = new NodeFilters();

        public NodeResolver Resolver { get; }

        public IProvider Provider => null;

        public bool IsChildOf(INode node) => false;

        public bool IsSourceChildOf(INode node) => false;

        public List<INode> GetParentalChain() => new List<INode> { this };

        public List<INode> GetSourceChain() => new List<INode> { this };
    }
}
