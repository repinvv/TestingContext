namespace TestingContextCore.Implementation.Providers
{
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.ResolutionContext;

    internal interface IProvider
    {
        Definition Definition { get; }

        IResolution Resolve(IResolutionContext parentContext);
    }
}
