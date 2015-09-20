namespace TestingContextCore.Implementation.ContextStorage
{
    internal class Swap
    {
        public Swap(Definition parent, Definition child, Definition dependedChild)
        {
            Parent = parent;
            Child = child;
            DependedChild = dependedChild;
        }

        public Definition Parent { get; }

        public Definition Child { get; }

        public Definition DependedChild { get; }

        public Swap Backward => new Swap(Parent, DependedChild, Child);

        public override bool Equals(object obj)
        {
            return Equals(obj as Swap);
        }

        public bool Equals(Swap other)
        {
            return other != null 
                && Equals(Parent, other.Parent) 
                && Child.Equals(other.Child) 
                && DependedChild.Equals(other.DependedChild);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Parent?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (Child?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (DependedChild?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}
