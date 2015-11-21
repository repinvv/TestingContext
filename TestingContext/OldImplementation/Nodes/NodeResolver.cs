namespace TestingContextCore.OldImplementation.Nodes
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.OldImplementation.ResolutionContext;
    using TestingContextCore.OldImplementation.TreeOperation;
    using TestingContextCore.OldImplementation.TreeOperation.Subsystems;

    internal class NodeResolver
    {
        internal delegate IEnumerable<IResolutionContext> Resolve(Definition definition, IResolutionContext context);
        private readonly Tree tree;
        private readonly Definition ownDefinition;
        private readonly Dictionary<Definition, List<INode>> nodeChains = new Dictionary<Definition, List<INode>>();
        private readonly Dictionary<Definition, Resolve> resolvers = new Dictionary<Definition, Resolve>();
        private readonly Dictionary<Definition, Definition> closestParents = new Dictionary<Definition, Definition>();
        private readonly Dictionary<Definition, Definition> closestSourceParents = new Dictionary<Definition, Definition>();
        private INode node;

        public NodeResolver(Tree tree, Definition ownDefinition)
        {
            this.tree = tree;
            this.ownDefinition = ownDefinition;
        }

        private INode Node => node ?? (node = tree.GetNode(ownDefinition));

        private List<INode> GetNodesChain(Definition definition)
        {
            return nodeChains.GetOrAdd(definition, () => tree.GetNode(definition).GetParentalChain());
        }

        private Resolve GetResolver(Definition definition)
        {
            var node = tree.GetNode(definition);
            if (node.IsChildOf(Node))
            {
                return ResolveDown;
            }

            if (!Node.IsChildOf(node))
            {
                return ResolveOtherBranch;
            }

            var chain = Node.GetSourceChain();
            return chain.Contains(node)
                ? (Resolve)ResolveSingleParent
                : ResolveSameBranch;
        }

        public IEnumerable<IResolutionContext> ResolveCollection(Definition definition, IResolutionContext context)
        {
            var resolver = resolvers.GetOrAdd(definition, () => GetResolver(definition));
            return resolver(definition, context);
        }

        private IEnumerable<IResolutionContext> ResolveDown(Definition definition, IResolutionContext context)
        {
            var chain = GetNodesChain(definition);
            return context.ResolveDown(definition, chain, chain.IndexOf(Node) + 1);
        }

        private IEnumerable<IResolutionContext> ResolveOtherBranch(Definition definition, IResolutionContext context)
        {
            var parent = closestParents.GetOrAdd(definition, () => GetClosestParent(definition));
            return context.ResolveFromClosestParent(definition, parent);
        }

        private IEnumerable<IResolutionContext> ResolveSingleParent(Definition definition, IResolutionContext context)
        {
            return new[] { context.ResolveSingle(definition) };
        }

        private IEnumerable<IResolutionContext> ResolveSameBranch(Definition definition, IResolutionContext context)
        {
            var parent = closestSourceParents.GetOrAdd(definition, () => GetClosestSourceParent(definition));
            var all = context.ResolveFromClosestParent(definition, parent);
            foreach (var resolutionContext in all)
            {
                var childItems = resolutionContext.Get(ownDefinition);
                if (childItems.Contains(context))
                {
                    yield return resolutionContext;
                }
            }
        }

        private Definition GetClosestParent(Definition definition)
        {
            var chain = tree.GetNode(definition).GetParentalChain();
            var thisChain = Node.GetParentalChain();
            var index = NodeClosestParentService.FindClosestParent(chain, thisChain);
            return chain[index].Definition;
        }

        private Definition GetClosestSourceParent(Definition definition)
        {
            var chain = tree.GetNode(definition).GetSourceChain();
            var thisChain = Node.GetSourceChain();
            var index = NodeClosestParentService.FindClosestParent(chain, thisChain);
            return chain[index].Definition;
        }
    }
}
