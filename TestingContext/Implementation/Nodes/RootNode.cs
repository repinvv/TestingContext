namespace TestingContextCore.Implementation.Nodes
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Providers;

    internal class RootNode : INode
    {
        public RootNode(IProvider provider, Definition definition)
        {
            Provider = provider;
            DefinitionChain = new List<Definition>();
        }

        public IProvider Provider { get; }

        public INode Root => this;

        public bool IsChildOf(INode node) => false;

        public List<Definition> DefinitionChain { get; }
    }
}
