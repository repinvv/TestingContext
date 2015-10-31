namespace TestingContextCore.Implementation.TreeOperation.Nodes
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Registrations;

    internal class Node : INode
    {
        private readonly AndGroup collectionGroup;
        private readonly AndGroup itemGroup;

        public Node(Definition definition, IProvider provider, bool collectionInvert, bool itemInvert)
        {
            Definition = definition;
            Provider = provider;
            collectionGroup = new AndGroup();
            itemGroup = new AndGroup();
            CollectionFilter = collectionInvert ? new Inverter(collectionGroup) as IFilter : collectionGroup;
            ItemFilter = itemInvert ? new Inverter(itemGroup) as IFilter : itemGroup;
        }

        public Definition Definition { get; }

        public INode Parent { get; set; }

        public List<INode> Children { get; } = new List<INode>();

        public bool IsChildOf(INode node) =>  Parent == node || Parent.IsChildOf(node);

        public IFilter CollectionFilter { get; }

        public IFilter ItemFilter { get; }

        public List<INode> GetNodesChain()
        {
            var list = Parent.GetNodesChain();
            list.Add(this);
            return list;
        }

        public IProvider Provider { get; }

        public void AddItemFilter(IFilter filter) => itemGroup.AddFilter(filter);

        public void AddCollectionFilter(IFilter filter) => collectionGroup.AddFilter(filter);

        public override string ToString() => Definition.ToString();

        public static Node CreateNode(Definition definition, IProvider provider, RegistrationStore store)
        {
            var collectionInvert = store.CollectionInversions.Contains(definition);
            var itemInvert = store.ItemInversions.Contains(definition);
            return new Node(definition, provider, collectionInvert, itemInvert);
        }
    }
}
