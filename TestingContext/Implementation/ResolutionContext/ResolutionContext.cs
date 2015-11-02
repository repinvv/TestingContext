namespace TestingContextCore.Implementation.ResolutionContext
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.TreeOperation.Nodes;
    using TestingContextCore.Interfaces;

    internal class ResolutionContext<T> : IResolutionContext<T>, IResolutionContext
    {
        private readonly INode node;
        private readonly IResolutionContext parent;
        private readonly Dictionary<Definition, IEnumerable<IResolutionContext>> childResolutions 
            = new Dictionary<Definition, IEnumerable<IResolutionContext>>();
        private readonly int[] failureWeight;
        private readonly IFailure failure;

        public ResolutionContext(T value,
            INode node,
            IResolutionContext parent)
        {
            Value = value;
            this.node = node;
            this.parent = parent;
            MeetsConditions = node.Filters.ItemFilter.MeetsCondition(this, node.Resolver, out failureWeight, out failure);
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

        public void ReportFailure(FailureCollect collect, int[] startingWeight)
        {
            collect.ReportFailure(startingWeight.Add(0).Add(failureWeight), failure);
            var children = childResolutions.Values.ToArray();
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i].Any(x => x.MeetsConditions))
                {
                    continue;
                }

                var cascadeWeight = startingWeight.Add(1, i);
                if (collect.CanCascade(cascadeWeight))
                {
                    foreach (var item in children[i])
                    {
                       item.ReportFailure(collect, cascadeWeight);   
                    }
                }
            }
        }
    }
}
