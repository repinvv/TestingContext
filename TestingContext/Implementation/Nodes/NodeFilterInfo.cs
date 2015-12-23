namespace TestingContextCore.Implementation.Nodes
{
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.TreeOperation;

    internal class NodeFilterInfo
    {
        public NodeFilterInfo(Tree tree, IToken token)
        {
            var and = new AndGroup { Filters = new List<IFilter>() };
            var inversionInfo = tree?.Store.ItemInversions.SafeGet(token);
            Group = and;
            ItemFilter = inversionInfo == null ? (IFilter)and : new Inverter(and, new FilterInfo(tree.Store.NextId, inversionInfo));
        }

        public IFilter ItemFilter { get; }

        public IFilterGroup Group { get; }
    }
}
