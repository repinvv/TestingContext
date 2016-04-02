namespace TestingContextCore.Implementation.Filters
{
    using TestingContextLimitedInterface.Diag;
    using TestingContextLimitedInterface.Tokens;

    internal abstract class BaseFilter
    {
        protected BaseFilter(FilterInfo filterInfo)
        {
            FilterInfo = filterInfo;
        }

        public FilterInfo FilterInfo { get; set; }

        public IDiagInfo Diagnostics => FilterInfo.DiagInfo;

        public IFilterToken ParentGroupToken => FilterInfo.ParentGroupToken;

        public IDiagInfo DiagInfo => FilterInfo.DiagInfo;

        public override string ToString()
        {
            return $"{GetType().Name}, Id: {FilterInfo.Id}";
        }
    }
}
