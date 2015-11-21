namespace TestingContextCore.Implementation.Filters
{
    internal interface IFilterGroup : IFilter
    {
        void AddFilter(IFilter filter);
    }
}
