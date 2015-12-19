namespace TestingContextCore.Implementation.Nodes
{
    using TestingContext.LimitedInterface.Diag;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;

    internal class NodeFilterInfo
    {
        public NodeFilterInfo(IDiagInfo inversionInfo)
        {
            var and = new AndGroup();
            Group = and;
            ItemFilter = inversionInfo == null ? (IFilter)and : new Inverter(and, inversionInfo);
        }

        public IFilter ItemFilter { get; }

        public IFilterGroup Group { get; }
    }
}
