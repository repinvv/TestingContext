namespace TestingContextCore.Implementation.Providers
{
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal interface IProvider
    {
        Definition Definition { get; }

        IResolution Resolve(IResolutionContext parentContext);
    }
}
