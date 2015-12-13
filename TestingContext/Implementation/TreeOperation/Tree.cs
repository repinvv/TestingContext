namespace TestingContextCore.Implementation.TreeOperation
{
    using System;
    using System.Collections.Generic;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Resolution;

    internal class Tree
    {
        public RootNode Root { get; set; }

        public Dictionary<IToken, INode> Nodes { get; } = new Dictionary<IToken, INode>();

        public IResolutionContext RootContext { get; set; }

        public List<Tuple<INode, IDiagInfo>> NodesToCreateExistsFilter { get; } = new List<Tuple<INode, IDiagInfo>>();

        public HashSet<Tuple<IToken, IToken>> NonEqualFilters { get; } = new HashSet<Tuple<IToken, IToken>>();

        public Dictionary<IFilter, int> FilterIndex { get; } = new Dictionary<IFilter, int>();
    }
}
