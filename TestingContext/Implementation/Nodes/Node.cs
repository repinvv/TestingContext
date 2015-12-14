namespace TestingContextCore.Implementation.Nodes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using TestingContext.LimitedInterface;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.TreeOperation;

    internal class Node : INode
    {
        private static int count;

        public Node(Tree tree, IToken token, IProvider provider, NodeFilterInfo filterInfo)
        {
            Id = count++;
            Token = token;
            Provider = provider;
            FilterInfo = filterInfo;
            Tree = tree;
            Resolver = new NodeResolver(this);
        }

        public int Id { get; }

        public Tree Tree { get; }

        public IToken Token { get; }

        public INode Parent { get; set; }
        
        public INode SourceParent { get; set; }

        public bool IsNegative { get; set; }

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

        public override string ToString() => $"{Token} Id: {Id}";

        public static Node CreateNode(IToken token, IProvider provider, TokenStore store, Tree tree)
        {
            return new Node(tree, token, provider, new NodeFilterInfo(store.ItemInversions.SafeGet(token)));
        }
    }
}
