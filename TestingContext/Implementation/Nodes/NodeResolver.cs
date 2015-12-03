namespace TestingContextCore.Implementation.Nodes
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.TreeOperation;
    using TestingContextCore.UsefulExtensions;
    using static TreeOperation.Subsystems.NodeClosestParentService;

    internal class NodeResolver
    {
        internal delegate IEnumerable<IResolutionContext> Resolve(IToken token, IResolutionContext context);
        private readonly Dictionary<IToken, List<INode>> nodeChains = new Dictionary<IToken, List<INode>>();
        private readonly Dictionary<IToken, IToken> closestParents = new Dictionary<IToken, IToken>();
        private readonly Dictionary<IToken, IToken> closestSourceParents = new Dictionary<IToken, IToken>();
        private readonly INode node;

        private readonly Dictionary<IToken, Resolve> resolvers = new Dictionary<IToken, Resolve>();
        private readonly Dictionary<IToken, Resolve> fitResolvers = new Dictionary<IToken, Resolve>();

        public NodeResolver(INode node)
        {
            this.node = node;
        }

        public IEnumerable<IResolutionContext> GetFitItems(IToken token, IResolutionContext context)
        {
            var resolver = fitResolvers.GetOrAdd(token, () => GetFitResolver(token));
            return resolver(token, context);
        }

        public IEnumerable<IResolutionContext> GetAllItems(IToken token, IResolutionContext context)
        {
            var resolver = resolvers.GetOrAdd(token, () => GetAllResolver(token));
            return resolver(token, context);
        }

        #region cached get implementers
        private Resolve GetAllResolver(IToken token)
        {
            var resolveNode = node.Tree.GetNode(token);
            if (resolveNode.IsChildOf(node))
            {
                return ResolveDown;
            }

            if (!node.IsChildOf(resolveNode))
            {
                return ResolveOtherBranch;
            }

            var chain = node.GetSourceChain();
            return chain.Contains(resolveNode) ? (Resolve)ResolveSingleParent : ResolveSameBranch;
        }

        private Resolve GetFitResolver(IToken token)
        {
            var resolveNode = node.Tree.GetNode(token);
            if (resolveNode.IsChildOf(node))
            {
                return GetFitMethod(ResolveDown);
            }

            if (!node.IsChildOf(resolveNode))
            {
                return GetFitMethod(ResolveOtherBranch);
            }

            var chain = node.GetSourceChain();
            return chain.Contains(resolveNode) ? (Resolve)ResolveSingleParent : GetFitMethod(ResolveSameBranch);
        }

        private Resolve GetFitMethod(Resolve input)
        {
            return (x, y) => input(x, y).Where(rc => rc.MeetsConditions).Distinct();
        }
        #endregion
        #region resolvers
        private IEnumerable<IResolutionContext> ResolveSingleParent(IToken token, IResolutionContext context)
        {
            return new[] { context.ResolveSingle(token) };
        }

        private IEnumerable<IResolutionContext> ResolveDown(IToken token, IResolutionContext context)
        {
            var chain = GetNodesChain(token);
            return context.ResolveDown(token, chain, chain.IndexOf(node) + 1);
        }

        private IEnumerable<IResolutionContext> ResolveOtherBranch(IToken token, IResolutionContext context)
        {
            var parent = closestParents.GetOrAdd(token, () => GetClosestParent(token));
            return context.ResolveFromClosestParent(token, parent);
        }
        
        private IEnumerable<IResolutionContext> ResolveSameBranch(IToken token, IResolutionContext context)
        {
            var parent = closestSourceParents.GetOrAdd(token, () => GetClosestSourceParent(token));
            var all = context.ResolveFromClosestParent(token, parent);
            return all.Where(resolutionContext => resolutionContext.GetFromTree(node.Token).Contains(context));
        }
        #endregion

        #region common methods
        private IToken GetClosestParent(IToken token)
        {
            var chain = node.Tree.GetNode(token).GetParentalChain();
            var thisChain = node.GetParentalChain();
            var index = FindClosestParent(chain, thisChain);
            return chain[index].Token;
        }

        private IToken GetClosestSourceParent(IToken token)
        {
            var chain = node.Tree.GetNode(token).GetSourceChain();
            var thisChain = node.GetSourceChain();
            var index = FindClosestParent(chain, thisChain);
            return chain[index].Token;
        }

        private List<INode> GetNodesChain(IToken token)
        {
            return nodeChains.GetOrAdd(token, () => node.Tree.GetNode(token).GetParentalChain());
        }
        #endregion
    }
}
