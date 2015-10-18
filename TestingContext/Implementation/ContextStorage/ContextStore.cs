namespace TestingContextCore.Implementation.ContextStorage
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;

    internal class ContextStore
    {
        public ContextStore(Definition rootDefinition)
        {
            RootDefinition = rootDefinition;
            RootNode = new RootNode(null, this);
            this.RegisterNode(RootDefinition, RootNode);
        }

        public event SearchFailureEventHandler OnSearchFailure;
        public Definition RootDefinition { get; }
        public INode RootNode { get; }

        public void SearchFailure(string entity, string filter, string key, bool inverted)
        {
            OnSearchFailure?.Invoke(this, new SearchFailureEventArgs { Entity = entity, FilterKey = key, FilterText = filter, Inverted = inverted });
        }
    }
}
