namespace TestingContextCore.Implementation.TreeOperation
{
    using TestingContextCore.Implementation.Filters;

    internal class NodeFilters
    {

        private readonly AndGroup collectionGroup;
        private readonly AndGroup itemGroup;
        public NodeFilters(bool collectionInvert = false, bool itemInvert = false)
        {
            collectionGroup = new AndGroup();
            itemGroup = new AndGroup();
            CollectionFilter = collectionInvert ? new Inverter(collectionGroup) as IFilter : collectionGroup;
            ItemFilter = itemInvert ? new Inverter(itemGroup) as IFilter : itemGroup;
        }

        public IFilter ItemFilter { get; }

        public IFilter CollectionFilter { get; }

        public void AddItemFilter(IFilter filter) => itemGroup.AddFilter(filter);

        public void AddCollectionFilter(IFilter filter) => collectionGroup.AddFilter(filter);
    }
}
