namespace TestingContextCore.Implementation
{
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.TreeOperation;
    using TestingContextCore.Implementation.TreeOperation.Nodes;

    internal class ProhibitedRelation
    {
        public ProhibitedRelation(INode parent, INode child, IFilter filter)
        {
            Parent = parent;
            Child = child;
            Filter = filter;
        }

        public INode Parent { get; }

        public INode Child { get; }

        public IFilter Filter { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as ProhibitedRelation);
        }

        public bool Equals(ProhibitedRelation other)
        {
            return !ReferenceEquals(other, null)
                && Parent.Definition == other.Parent.Definition
                && Child.Definition == other.Child.Definition;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Parent.Definition.GetHashCode();
                hashCode = (hashCode * 397) ^ (Child.Definition.GetHashCode());
                return hashCode;
            }
        }
    }
}
