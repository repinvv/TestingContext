namespace TestingContextCore.Implementation.Filters
{
    using TestingContextCore.Implementation.ResolutionContext;

    internal interface IFilter
    {
        bool IsPostFilter { get; }

        bool IsCollectionFilter { get; }

        Definition[] Definitions { get; }

        bool MeetsCondition(IResolutionContext context);
    }
}
