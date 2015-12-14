namespace TestingContextCore.Implementation.Filters
{
    using global::TestingContext.LimitedInterface.Diag;
    using global::TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Tokens;

    internal abstract class BaseFilter
    {
        protected BaseFilter(IFilterGroup group, IDiagInfo diagInfo)
        {
            Group = group;
            DiagInfo = diagInfo;
        }

        public int Id { get; set; }
        public IFilterToken Token { get; } = new FilterToken();
        public IFilterGroup Group { get; set; }
        public IDiagInfo DiagInfo { get; }
        public IDiagInfo Diagnostics => DiagInfo;

        public override string ToString()
        {
            return $"{GetType().Name}, Id: {Id}";
        }
    }
}
