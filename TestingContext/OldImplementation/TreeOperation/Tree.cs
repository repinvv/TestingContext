namespace TestingContextCore.OldImplementation.TreeOperation
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.OldImplementation.Nodes;
    using TestingContextCore.OldImplementation.ResolutionContext;

    internal class Tree
    {
        public RootNode Root { get; set; }

        public Dictionary<Definition, INode> Nodes { get; } = new Dictionary<Definition, INode>();

        public HashSet<BranchReference> BranchReferences { get; } = new HashSet<BranchReference>();

        public IResolutionContext RootContext { get; set; }

        public HashSet<Tuple<Definition, Definition>> NonEqualFilters { get; } = new HashSet<Tuple<Definition, Definition>>();
    }
}
