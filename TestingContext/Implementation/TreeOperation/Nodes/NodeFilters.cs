namespace TestingContextCore.Implementation.TreeOperation.Nodes
{
    using TestingContextCore.Implementation.Filters;

    internal class NodeFilters
    {
        private readonly AndGroup itemGroup;
        public NodeFilters(bool itemInvert = false)
        {
            itemGroup = new AndGroup();
            ItemFilter = itemInvert ? new Inverter(itemGroup) as IFilter : itemGroup;
        }

        public IFilter ItemFilter { get; }

        public void AddItemFilter(IFilter filter) => itemGroup.AddFilter(filter);
    }
}
