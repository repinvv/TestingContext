namespace TestingContextCore.Implementation.Nodes
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Providers;

    internal class Node : INode
    {
        private readonly Definition definition;
        private readonly Definition parentDefinition;
        private readonly ContextStore store;
        private INode parent;
        private List<Definition> chain;
        private INode root;

        public Node(IProvider provider, Definition definition, Definition parentDefinition, ContextStore store)
        {
            Provider = provider;
            this.definition = definition;
            this.parentDefinition = parentDefinition;
            this.store = store;
        }

        public IProvider Provider { get; }

        public INode Root => root = root ?? Parent.Root ?? this;

        public bool IsChildOf(INode node)
        {
            return Parent == node || Parent.IsChildOf(node);
        }

        public List<Definition> DefinitionChain => chain = chain ?? new List<Definition>(Parent.DefinitionChain) { definition };

        public INode Parent => parent = parent ?? store.GetNode(parentDefinition);
    }
}
