namespace TestingContextCore.OldImplementation.Dependencies
{
    internal class DummyDependency : IDependency
    {
        public DummyDependency(Definition definition, DependencyType type)
        {
            Definition = definition;
            Type = type;
        }

        public Definition Definition { get; }

        public DependencyType Type { get; }
    }
}
