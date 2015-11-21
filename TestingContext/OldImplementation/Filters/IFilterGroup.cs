namespace TestingContextCore.OldImplementation.Filters
{
    internal interface IFilterGroup : IFilter
    {
        void AddFilter(IFilter filter);
    }
}
