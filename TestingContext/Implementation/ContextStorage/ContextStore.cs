namespace TestingContextCore.Implementation.ContextStorage
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Providers;

    internal class ContextStore
    {
        public ContextStore(Definition rootDefinition)
        {
            RootDefinition = rootDefinition;
        }

        public event SearchFailureEventHandler OnSearchFailure;
        public Definition RootDefinition { get; }

        public void SearchFailure(string entity, string filter, string key, bool inverted)
        {
            OnSearchFailure?
                .Invoke(this,
                        new SearchFailureEventArgs
                        {
                            Entity = entity,
                            FilterKey = key,
                            FilterText = filter,
                            Inverted = inverted
                        });
        }

        public IDictionary<Definition, IProvider> Providers { get; } = new Dictionary<Definition, IProvider>();

        public List<IFilter> Filters { get; } = new List<IFilter>();
    }
}
