namespace TestingContextCore.Implementation.Nodes
{
    using TestingContext.Interface;
    using TestingContextCore.Implementation.Filters;

    internal class NodeFilterInfo
    {
        public NodeFilterInfo(IDiagInfo inversionInfo)
        {
            var and = new AndGroup();
            Group = and;
            ItemFilter = inversionInfo == null ? (IFilter)and : new NotGroup(and, inversionInfo);
        }

        public IFilter ItemFilter { get; }

        public IFilterGroup Group { get; }
    }
}
