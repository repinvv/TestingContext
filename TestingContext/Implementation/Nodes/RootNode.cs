namespace TestingContextCore.Implementation.Nodes
{
    using TestingContextCore.Implementation.Providers;

    internal class RootNode : INode
    {
        public RootNode(IProvider provider, Definition definition)
        {
            this.Provider = provider;
        }

        public IProvider Provider { get; }

        public INode Root => this;

        public bool IsChildOf(INode node) => false;
    }
}
