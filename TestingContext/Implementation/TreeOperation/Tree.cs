namespace TestingContextCore.Implementation.TreeOperation
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces.Tokens;

    internal class Tree
    {
        public RootNode Root { get; set; }

        public Dictionary<IToken, INode> Nodes { get; } = new Dictionary<IToken, INode>();

        public IResolutionContext RootContext { get; set; }

        public HashSet<Tuple<IToken, IToken>> NonEqualFilters { get; } = new HashSet<Tuple<IToken, IToken>>();
    }
}
