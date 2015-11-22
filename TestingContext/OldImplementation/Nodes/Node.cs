﻿namespace TestingContextCore.OldImplementation.Nodes
{
    using System.Collections.Generic;
    using TestingContextCore.OldImplementation.Providers;
    using TestingContextCore.OldImplementation.Registrations;
    using TestingContextCore.OldImplementation.TreeOperation;

    internal class Node : INode
    {
        public Node(Tree tree, Definition definition, IProvider provider, NodeFilters filters)
        {
            Definition = definition;
            Provider = provider;
            Filters = filters;
            Resolver = new NodeResolver(tree, definition);
        }

        public Definition Definition { get; }

        public INode Parent { get; set; }

        public INode SourceParent { get; set; }

        public bool IsChildOf(INode node) => Parent == node || Parent.IsChildOf(node);

        public bool IsSourceChildOf(INode node) => SourceParent == node || SourceParent.IsSourceChildOf(node);

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

        public NodeFilters Filters { get; }

        public NodeResolver Resolver { get; }

        public IProvider Provider { get; }

        public override string ToString() => Definition.ToString();

        public static Node CreateNode(Definition definition, IProvider provider, RegistrationStore store, Tree tree)
        {
            var itemInvert = store.ItemInversions.Contains(definition);
            return new Node(tree, definition, provider, new NodeFilters(itemInvert));
        }
    }
}