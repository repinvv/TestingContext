namespace TestingContextCore.Implementation.Nodes
{
    using TestingContextCore.Implementation.Sources;

    internal class RootNode : INode
    {
        public RootNode(ISource source, Definition definition)
        {
            this.Source = source;
        }

        public ISource Source { get; }

        public INode Root => this;

        public bool IsChildOf(INode node) => false;
    }
}
