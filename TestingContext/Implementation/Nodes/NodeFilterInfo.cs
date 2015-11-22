namespace TestingContextCore.Implementation.Nodes
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    internal class NodeFilterInfo
    {
        public NodeFilterInfo(DiagInfo inversionInfo)
        {
            var and = new AndGroup();
            Group = and;
            ItemFilter = inversionInfo == null ? (IFilter)and : new Inverter(and, inversionInfo);
        }

        public IFilter ItemFilter { get; }

        public IFilterGroup Group { get; }
    }
}
