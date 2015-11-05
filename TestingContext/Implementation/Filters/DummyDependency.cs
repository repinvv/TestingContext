namespace TestingContextCore.Implementation.Filters
{
    using TestingContextCore.Implementation.Dependencies;

    internal class DummyDependency : IDependency
    {
        public DummyDependency(Definition definition, bool isCollectionDependency)
        {
            Definition = definition;
            IsCollectionDependency = isCollectionDependency;
        }

        public Definition Definition { get; }
        public bool IsCollectionDependency { get; }
    }
}
