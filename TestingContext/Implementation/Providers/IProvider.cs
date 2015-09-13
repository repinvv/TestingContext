namespace TestingContextCore.Implementation.Providers
{
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;

    internal interface IProvider
    {
        IResolution Resolve(IResolutionContext parentContext);
    }
}
