namespace TestingContextCore.Implementation.Nodes
{
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.TreeOperation;

    internal class NodeFilterInfo
    {
        public NodeFilterInfo(TokenStore store, IToken token)
        {
            var and = new AndGroup { Filters = new List<IFilter>() };
            var inversionInfo = store?.ItemInversions.SafeGet(token);
            Group = and;
            
            ItemFilter = inversionInfo == null ? (IFilter)and : new Inverter(and, new FilterInfo(0, inversionInfo));
        }

        public IFilter ItemFilter { get; }

        public IFilterGroup Group { get; }
    }
}
