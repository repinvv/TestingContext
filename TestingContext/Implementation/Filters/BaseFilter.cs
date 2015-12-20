namespace TestingContextCore.Implementation.Filters
{
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Tokens;

    internal abstract class BaseFilter
    {
        protected BaseFilter(FilterInfo filterInfo)
        {
            FilterInfo = filterInfo;
        }

        public FilterInfo FilterInfo { get; set; }

        public IDiagInfo Diagnostics => FilterInfo.DiagInfo;

        public IFilterToken GroupToken => FilterInfo.GroupToken;

        public IDiagInfo DiagInfo => FilterInfo.DiagInfo;

        public override string ToString()
        {
            return $"{GetType().Name}, Id: {FilterInfo.Id}";
        }
    }
}
