namespace TestingContextCore.Implementation.Providers
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;

    internal class ProviderDetails
    {
        private readonly ContextStore store;
        private List<IFilter> filters;
        private List<IFilter> postFilters;
        private List<IFilter> collectionFilters;
        private List<IProvider> childProviders;

        public ProviderDetails(ContextStore store, Definition definition)
        {
            this.store = store;
            Definition = definition;
        }

        public Definition Definition { get; }

        public List<IFilter> PostFilters => postFilters = postFilters ?? store.GetFilters(Definition)
                                                                              .Where(x => x.IsPostFilter)
                                                                              .ToList();

        public List<IFilter> CollectionFilters => collectionFilters = collectionFilters ?? store.GetFilters(Definition)
                                                                                                .Where(x => x.IsCollectionFilter)
                                                                                                .ToList();

        public List<IFilter> Filters => filters = filters ?? store.GetFilters(Definition)
                                                                  .Except(PostFilters)
                                                                  .Except(CollectionFilters)
                                                                  .ToList();

        public List<IProvider> ChildProviders => childProviders = childProviders
                                                    ?? store.Dependendents.SafeGet(Definition, new List<INode>())
                                                            .Select(x => x.Provider)
                                                            .ToList();
    }
}
