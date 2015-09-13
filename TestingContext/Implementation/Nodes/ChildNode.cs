namespace TestingContextCore.Implementation.Nodes
{
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Providers;

    internal class ChildNode : INode
    {
        private readonly Definition parentDefinition;
        private readonly ContextStore store;
        private INode parent;

        public ChildNode(IProvider provider, Definition definition, Definition parentDefinition, ContextStore store)
        {
            Provider = provider;
            this.parentDefinition = parentDefinition;
            this.store = store;
        }

        public IProvider Provider { get; }

        public INode Root => Parent.Root;

        public bool IsChildOf(INode node)
        {
            return Parent == node || Parent.IsChildOf(node);
        }

        public INode Parent => parent = parent ?? store.GetNode(parentDefinition);
    }
}
