namespace TestingContextCore.Implementation.Nodes
{
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.PublicMembers;

    internal class NodeFilterInfo
    {
        public NodeFilterInfo(DiagInfo inversionInfo)
        {
            var and = new AndGroup();
            Group = and;
            ItemFilter = inversionInfo == null ? (IFilter)and : new NotGroup(and, inversionInfo);
        }

        public IFilter ItemFilter { get; }

        public IFilterGroup Group { get; }
    }
}
