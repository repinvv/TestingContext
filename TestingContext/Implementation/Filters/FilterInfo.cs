namespace TestingContextCore.Implementation.Filters
{
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.PublicMembers;
    using TestingContextLimitedInterface.Diag;
    using TestingContextLimitedInterface.Tokens;

    internal class FilterInfo
    {
        public IFilterToken ParentGroupToken { get; set; }
        public IFilterToken FilterToken { get; }
        public IDiagInfo DiagInfo { get; }
        public int Priority { get; }
        public int Id { get; }

        public FilterInfo(int id, IDiagInfo diagInfo = null,
            IFilterToken parentGroupToken = null,
            int priority = TestingContext.DefaultPriority)
        {
            FilterToken = new FilterToken();
            ParentGroupToken = parentGroupToken;
            Priority = priority;
            DiagInfo = diagInfo;
            Id = id;
        }
    }
}
