namespace TestingContextCore.Implementation.Filters
{
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;

    internal interface IFilter
    {
        Definition[] Definitions { get; }

        bool MeetsCondition(IResolutionContext resolve);
    }
}
