namespace TestingContextCore.Implementation.Filters
{
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.ResolutionContext;

    internal interface IFilter : IFailure
    {
        bool IsCollectionFilter { get; }

        Definition[] Definitions { get; }

        bool MeetsCondition(IResolutionContext context);

        void Invert();
    }
}
