namespace TestingContextCore.Implementation.TreeOperation
{
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Resolution;

    internal class Tree
    {
        public RootNode Root { get; set; }

        public IResolutionContext RootContext { get; set; }

        public Dictionary<IToken, INode> Nodes { get; set; }

        public Dictionary<IFilter, int> FilterIndex { get; set; }
    }
}
