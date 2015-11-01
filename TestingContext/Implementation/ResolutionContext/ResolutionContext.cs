namespace TestingContextCore.Implementation.ResolutionContext
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.TreeOperation.Nodes;
    using TestingContextCore.Interfaces;

    internal class ResolutionContext<T> : IResolutionContext<T>, IResolutionContext
    {
        private readonly INode node;
        private readonly IResolutionContext parent;
        private readonly Dictionary<Definition, IEnumerable<IResolutionContext>> childResolutions 
            = new Dictionary<Definition, IEnumerable<IResolutionContext>>();

        public ResolutionContext(T value,
            INode node,
            IResolutionContext parent)
        {
            Value = value;
            this.node = node;
            this.parent = parent;
            MeetsConditions = node.Filters.ItemFilter.MeetsCondition(this, node.Resolver);
        }

        public bool MeetsConditions { get; }

        public T Value { get; }

        public IEnumerable<IResolutionContext<T2>> Get<T2>(string key)
        {
            yield break;
        }

        public IEnumerable<IResolutionContext> ResolveDown(Definition definition, List<INode> chain, int index)
        {
            var nextNode = chain[index];
            var resolution = GetChildResolution(nextNode);
            if (definition == nextNode.Definition)
            {
                return resolution;
            }

            return resolution
                .Where(x => x.MeetsConditions)
                .SelectMany(x => x.ResolveDown(definition, chain, index + 1));
        }

        public IResolutionContext ResolveSingle(Definition definition) => definition == node.Definition ? this : parent.ResolveSingle(definition);

        private IEnumerable<IResolutionContext> GetChildResolution(INode nextNode)
        {
            return childResolutions.GetOrAdd(nextNode.Definition, () => nextNode.Provider.Resolve(this, nextNode));
        }
    }
}
