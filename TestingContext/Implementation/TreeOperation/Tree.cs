namespace TestingContextCore.Implementation.TreeOperation
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextLimitedInterface.Tokens;

    internal class Tree
    {
        public RootNode Root { get; set; }

        public IResolutionContext RootContext { get; set; }

        public Dictionary<IToken, INode> Nodes { get; set; }

        public Dictionary<IFilter, int> FilterIndex { get; set; }
    }
}
