namespace TestingContextCore.Implementation.Filters
{
    using TestingContextCore.Implementation.Resolution;

    internal interface IFilter
    {
        EntityDefinition[] EntityDefinitions { get; }

        bool MeetsCondition(IResolve resolve);
    }
}
