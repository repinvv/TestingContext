namespace TestingContextCore.Implementation.Providers
{
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.ResolutionContext;

    internal interface IProvider
    {
        IDependency Dependency { get; }

        IResolution Resolve(IResolutionContext parentContext);
    }
}
