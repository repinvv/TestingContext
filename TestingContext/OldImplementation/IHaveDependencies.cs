namespace TestingContextCore.OldImplementation
{
    using TestingContextCore.OldImplementation.Dependencies;

    internal interface IHaveDependencies
    {
        IDependency[] Dependencies { get; }
    }
}
