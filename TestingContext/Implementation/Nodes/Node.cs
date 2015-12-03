namespace TestingContextCore.Implementation.Nodes
{
    using System.Collections.Generic;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.TreeOperation;
    using TestingContextCore.UsefulExtensions;

    internal class Node : INode
    {
        public Node(Tree tree, IToken token, IProvider provider, NodeFilterInfo filterInfo)
        {
            Token = token;
            Provider = provider;
            FilterInfo = filterInfo;
            Tree = tree;
            Resolver = new NodeResolver(this);
        }

        public Tree Tree { get; }

        public IToken Token { get; }

        public INode Parent { get; set; }
        
        public INode SourceParent { get; set; }

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

        public NodeFilterInfo FilterInfo { get; }

        public NodeResolver Resolver { get; }

        public IProvider Provider { get; }

        public override string ToString() => Token.ToString();

        public static Node CreateNode(IToken token, IProvider provider, TokenStore store, Tree tree)
        {
            return new Node(tree, token, provider, new NodeFilterInfo(store.ItemInversions.SafeGet(token)));
        }
    }
}
