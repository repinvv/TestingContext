namespace TestingContextCore.Implementation.Filters
{
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Tokens;

    internal abstract class BaseFilter
    {
        protected BaseFilter(IFilterGroup group, IDiagInfo diagInfo, int id)
        {
            Group = group;
            DiagInfo = diagInfo;
            Id = id;
        }

        public int Id { get; }
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
