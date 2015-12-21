namespace TestingContextCore.Implementation.TreeOperation
{
    using System;
    using System.Collections.Generic;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Resolution;

    internal class Tree
    {
        public TokenStore Store { get; set; }

        public RootNode Root { get; set; }

        public IResolutionContext RootContext { get; set; }

        public Dictionary<IToken, INode> Nodes { get; } = new Dictionary<IToken, INode>();

        public List<INode> GroupNodes { get; } = new List<INode>();

        public Dictionary<IToken, HashSet<IToken>> Parents { get; } = new Dictionary<IToken, HashSet<IToken>>();


        public List<IFilter> Filters { get; set; }

        public Dictionary<IFilterToken, IFilterGroup> FilterGroups { get; } = new Dictionary<IFilterToken, IFilterGroup>();

        // used to avoid assignin two or more equal filters to the same pair of nodes
        public HashSet<Tuple<IToken, IToken>> NonEqualFilters { get; } = new HashSet<Tuple<IToken, IToken>>();

        // used to determine the failing filter by order of the assignment
        // filters will be assigned in order of defined priority, 
        // and, in case of equal priority, in order of declaration
        public Dictionary<IFilter, int>  FilterIndex { get; } = new Dictionary<IFilter, int>();
    }
}
