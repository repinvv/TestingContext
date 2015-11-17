namespace TestingContextCore.Implementation.TreeOperation
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.ResolutionContext;

    internal class Tree
    {
        public RootNode Root { get; set; }

        public Dictionary<Definition, INode> Nodes { get; } = new Dictionary<Definition, INode>();

        public HashSet<BranchReference> BranchReferences { get; } = new HashSet<BranchReference>();

        public IResolutionContext RootContext { get; set; }
    }
}
