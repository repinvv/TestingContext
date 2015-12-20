namespace TestingContextCore.Implementation.Nodes
{
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Diag;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;

    internal class NodeFilterInfo
    {
        public NodeFilterInfo(IDiagInfo inversionInfo)
        {
            var and = new AndGroup { Filters = new List<IFilter>() };
            Group = and;
            ItemFilter = inversionInfo == null ? (IFilter)and : new Inverter(and, new FilterInfo(inversionInfo));
        }

        public IFilter ItemFilter { get; }

        public IFilterGroup Group { get; }
    }
}
