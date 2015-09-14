namespace TestingContextCore.Implementation.Filters
{
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Interfaces;

    internal interface IFilter
    {
        Definition[] Definitions { get; }

        bool MeetsCondition(IResolutionContext context);
    }
}
