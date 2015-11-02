namespace TestingContextCore.Implementation.TreeOperation.Nodes
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ResolutionContext;

    internal class NodeResolver
    {
        private readonly Tree tree;
        private readonly Definition ownDefinition;
        private readonly Dictionary<Definition, List<INode>> nodeChains = new Dictionary<Definition, List<INode>>();
        private INode node;

        public NodeResolver(Tree tree, Definition ownDefinition)
        {
            this.tree = tree;
            this.ownDefinition = ownDefinition;
        }

        private INode Node => node ?? (node = tree.Nodes[ownDefinition]);

        private List<INode> GetNodesChain(Definition definition)
        {
            return nodeChains.GetOrAdd(definition, () => tree.Nodes[definition].GetNodesChain());
        }

        public IEnumerable<IResolutionContext> ResolveCollection(Definition definition, IResolutionContext context)
        {
            return ResolveDown(definition, context);
        }

        private IEnumerable<IResolutionContext> ResolveDown(Definition definition, IResolutionContext context)
        {
            var chain = GetNodesChain(definition);
            return context.ResolveDown(definition, chain, chain.IndexOf(Node) + 1);
        }
    }
}
