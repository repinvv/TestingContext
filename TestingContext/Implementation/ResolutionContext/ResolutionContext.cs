namespace TestingContextCore.Implementation.ResolutionContext
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.TreeOperation.Nodes;
    using TestingContextCore.Interfaces;

    internal class ResolutionContext<T> : IResolutionContext<T>, IResolutionContext
    {
        private readonly Definition ownDefinition;
        private readonly IResolutionContext parent;
        private Dictionary<Definition, INode> childNodes;
        private Dictionary<Definition, IEnumerable<IResolutionContext<T>>> childResolutions
            = new Dictionary<Definition, IEnumerable<IResolutionContext<T>>>();

        public ResolutionContext(T value,
            Definition ownDefinition,
            INode node,
            IResolutionContext parent)
        {
            this.ownDefinition = ownDefinition;
            this.parent = parent;
            childNodes = node.Children.ToDictionary(x => x.Definition);
            Value = value;
            MeetsConditions = node.Filters.ItemFilter.MeetsCondition(this);
        }

        public bool MeetsConditions { get; }

        public T Value { get; }

        public IEnumerable<IResolutionContext<T2>> Get<T2>(string key)
        {
            yield break;
        }

        public IResolutionContext ResolveSingle(Definition definition)
        {
            return null;
        }

        public IEnumerable<IResolutionContext> ResolveDown(Definition definition)
        {
            yield break;
        }

        public IEnumerable<IResolutionContext> ResolveDown(Definition definition, List<Definition> chain, int nextIndex)
        {
            yield break;
        }
    }
}
