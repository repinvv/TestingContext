namespace TestingContextCore.OldImplementation.Nodes
{
    using TestingContextCore.OldImplementation.Filters;

    internal class NodeFilters
    {
        public NodeFilters(bool itemInvert = false)
        {
            var and = new AndGroup();
            Group = and;
            ItemFilter = itemInvert ? new Inverter(and) as IFilter : and;
        }

        public IFilter ItemFilter { get; }

        public IFilterGroup Group { get; }
    }
}
