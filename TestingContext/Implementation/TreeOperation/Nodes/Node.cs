namespace TestingContextCore.Implementation.TreeOperation.Nodes
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Registrations;

    internal class Node : INode
    {
        public Node(Tree tree, Definition definition, IProvider provider, NodeFilters filters)
        {
            Definition = definition;
            Provider = provider;
            Filters = filters;
            Resolver = new NodeResolver(tree);
        }

        public Definition Definition { get; }

        public INode Parent { get; set; }

        public List<INode> Children { get; } = new List<INode>();

        public bool IsChildOf(INode node) =>  Parent == node || Parent.IsChildOf(node);

        public List<INode> GetNodesChain()
        {
            var list = Parent.GetNodesChain();
            list.Add(this);
            return list;
        }

        public NodeFilters Filters { get; }

        public NodeResolver Resolver { get; }

        public IProvider Provider { get; }

        public override string ToString() => Definition.ToString();

        public static Node CreateNode(Definition definition, IProvider provider, RegistrationStore store, Tree tree)
        {
            var collectionInvert = store.CollectionInversions.Contains(definition);
            var itemInvert = store.ItemInversions.Contains(definition);
            return new Node(tree, definition, provider, new NodeFilters(collectionInvert, itemInvert));
        }
    }
}
