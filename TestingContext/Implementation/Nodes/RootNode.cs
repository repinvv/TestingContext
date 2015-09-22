namespace TestingContextCore.Implementation.Nodes
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Providers;

    internal class RootNode : INode
    {
        private readonly ContextStore store;

        public RootNode(IProvider provider, Definition definition, ContextStore store)
        {
            this.store = store;
            Provider = provider;
            DefinitionChain = new List<Definition> { definition };
        }

        public IProvider Provider { get; }

        public INode Root => null;

        public bool IsChildOf(INode node) => false;

        public List<Definition> DefinitionChain { get; }
    }
}
