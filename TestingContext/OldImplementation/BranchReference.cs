namespace TestingContextCore.OldImplementation
{
    internal class BranchReference
    {
        public BranchReference(Definition parent, Definition child, Definition dependedChild)
        {
            Parent = parent;
            Child = child;
            DependedChild = dependedChild;
        }

        public Definition Parent { get; }

        public Definition Child { get; }

        public Definition DependedChild { get; }

        public BranchReference Backward => new BranchReference(Parent, DependedChild, Child);

        public override bool Equals(object obj)
        {
            return Equals(obj as BranchReference);
        }

        public bool Equals(BranchReference other)
        {
            return !ReferenceEquals(other, null)
                && Parent == other.Parent
                && Child == other.Child
                && DependedChild == other.DependedChild;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Parent.GetHashCode();
                hashCode = (hashCode * 397) ^ (Child.GetHashCode());
                hashCode = (hashCode * 397) ^ (DependedChild.GetHashCode());
                return hashCode;
            }
        }
    }
}
