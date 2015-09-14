namespace TestingContextCore.Implementation.ResolutionContext
{
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;

    internal interface IResolutionContext
    {
        IResolution Resolve(Definition definition);

        IResolutionContext GetContext(Definition contextDef);
    }

    internal interface IInternalResolutionContext<T> : IResolutionContext<T>
    {
        bool MeetsConditions { get; }
    }
}
