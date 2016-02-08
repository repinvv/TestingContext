namespace TestingContextCore.Implementation.Nodes
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.TreeOperation;

    internal class Node : INode
    {
        private readonly int id;

        public Node(Tree tree, IToken token, IProvider provider, bool isNegative, NodeFilterInfo filterInfo, int id)
        {
            this.id = id;
            Token = token;
            Provider = provider;
            IsNegative = isNegative;
            FilterInfo = filterInfo;
            Tree = tree;
            Resolver = new NodeResolver(this);
        }

        public int Weight { get; set; } = 0;

        public Tree Tree { get; }

        public IToken Token { get; }

        public INode Parent { get; set; }
        
        public INode SourceParent { get; set; }

        public bool IsNegative { get; }

        public bool IsChildOf(INode node) => Parent == node || Parent.IsChildOf(node);

        public List<INode> GetParentalChain()
        {
            var list = Parent.GetParentalChain();
            list.Add(this);
            return list;
        }

        public List<INode> GetSourceChain()
        {
            var list = SourceParent.GetSourceChain();
            list.Add(this);
            return list;
        }

        public List<INode> Children => Tree.Nodes.Select(x => x.Value).Where(x => x.Parent == this).ToList();

        public NodeFilterInfo FilterInfo { get; }

        public NodeResolver Resolver { get; }

        public IProvider Provider { get; }

        public override string ToString() => $"{Token} Id: {id}";

        public static Node CreateNode(IToken token, IProvider provider, bool isNegative, TreeContext context)
        {
            return new Node(context.Tree, token, provider, isNegative, new NodeFilterInfo(context.Store, token), context.NextId);
        }
    }
}
