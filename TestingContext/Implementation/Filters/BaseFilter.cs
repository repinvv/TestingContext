namespace TestingContextCore.Implementation.Filters
{
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Tokens;

    internal abstract class BaseFilter
    {
        protected BaseFilter(IDiagInfo diagInfo)
        {
            DiagInfo = diagInfo;
        }
        
        public int Id { get; set; }
        public IFilterToken Token { get; } = new FilterToken();
        public IDiagInfo DiagInfo { get; }
        public IDiagInfo Diagnostics => DiagInfo;

        public override string ToString()
        {
            return $"{GetType().Name}, Id: {Id}";
        }
    }
}
