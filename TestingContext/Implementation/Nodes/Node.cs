﻿namespace TestingContextCore.Implementation.Nodes
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Registration;
    using TestingContextCore.Implementation.TreeOperation;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.UsefulExtensions;

    internal class Node : INode
    {
        public Node(Tree tree, IToken token, IProvider provider, NodeFilterInfo filterInfo)
        {
            Token = token;
            Provider = provider;
            FilterInfo = filterInfo;
            //Resolver = new NodeResolver(tree, token);
        }

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

        public NodeFilterInfo FilterInfo { get; }

        public IProvider Provider { get; }

        public override string ToString() => Token.ToString();

        public static Node CreateNode(IToken token, IProvider provider, TokenStore store, Tree tree)
        {
            return new Node(tree, token, provider, new NodeFilterInfo(store.ItemInversions.SafeGet(token)));
        }
    }
}
