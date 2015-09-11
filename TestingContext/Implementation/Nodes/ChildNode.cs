namespace TestingContextCore.Implementation.Nodes
{
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Sources;

    internal class ChildNode : INode
    {
        private readonly Definition parentDefinition;
        private readonly ContextStore store;
        private INode parent;

        public ChildNode(ISource source, Definition definition, Definition parentDefinition, ContextStore store)
        {
            Source = source;
            this.parentDefinition = parentDefinition;
            this.store = store;
        }

        public ISource Source { get; }

        public INode Root => Parent.Root;

        public bool IsChildOf(INode node)
        {
            return Parent == node || Parent.IsChildOf(node);
        }

        public INode Parent => parent = parent ?? store.GetNode(parentDefinition);
    }
}
