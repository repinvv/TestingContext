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

        public static bool operator ==(Swap swap, Swap other)
        {
            return swap == null && other == null || swap != null && swap.Equals(other);
        }

        public static bool operator !=(Swap swap, Swap other)
        {
            return !(swap == other);
        }
    }
}
