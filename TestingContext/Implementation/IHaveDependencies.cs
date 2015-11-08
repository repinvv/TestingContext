namespace TestingContextCore.Implementation
{
    using TestingContextCore.Implementation.Dependencies;

    internal interface IHaveDependencies
    {
        IDependency[] Dependencies { get; }
    }
}
