namespace TestingContextCore.Implementation.TreeOperation
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Implementation.TreeOperation.Nodes;

    internal class Tree
    {
        public RootNode Root { get; set; }

        public Dictionary<Definition, INode> Nodes { get; } = new Dictionary<Definition, INode>();

        public HashSet<ProhibitedRelation> ProhibitedRelations { get; } = new HashSet<ProhibitedRelation>();

        public HashSet<BranchReference> BranchReferences { get; } = new HashSet<BranchReference>();

        public IResolutionContext RootContext { get; set; }
    }
}
