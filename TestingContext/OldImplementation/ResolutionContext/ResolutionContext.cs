﻿namespace TestingContextCore.OldImplementation.ResolutionContext
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.FailureInfo;
    using TestingContextCore.OldImplementation.Logging;
    using TestingContextCore.OldImplementation.Nodes;

    internal class ResolutionContext<T> : IResolutionContext<T>, IResolutionContext
    {
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
            Node = node;
            this.parent = parent;
            MeetsConditions = node.Filters.ItemFilter.MeetsCondition(this, node.Resolver, out failureWeight, out failure);
        }

        public bool MeetsConditions { get; }

        public INode Node { get; }

        public T Value { get; }

        public IEnumerable<IResolutionContext<T2>> Get<T2>(string key)
        {
            return Get(Definition.Define<T2>(key, Node.Definition.Scope))
                .Distinct()
                .Cast<IResolutionContext<T2>>();
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

        public IResolutionContext ResolveSingle(Definition definition) => definition == Node.Definition ? this : parent.ResolveSingle(definition);

        public IEnumerable<IResolutionContext> ResolveFromClosestParent(Definition definition, Definition parentDefinition)
        {
            return parentDefinition == Node.Definition 
                ? Get(definition) 
                : parent.ResolveFromClosestParent(definition, parentDefinition);
        }

        public IEnumerable<IResolutionContext> Get(Definition definition)
        {
            return Node.Resolver.ResolveCollection(definition, this)
                       .Where(x => x.MeetsConditions);
        }

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

        public override bool Equals(object obj)
        {
            return Equals(obj as ResolutionContext<T>);
        }

        protected bool Equals(ResolutionContext<T> other)
        {
            return other != null && ReferenceEquals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            return RuntimeHelpers.GetHashCode(Value);
        }
    }
}