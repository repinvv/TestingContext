namespace TestingContextCore.Implementation.Filters
{
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.PublicMembers;

    internal class FilterInfo
    {
        public IFilterToken GroupToken { get; set; }
        public IFilterToken Token { get; }
        public IDiagInfo DiagInfo { get; }
        public int Priority { get; }
        public int Id { get; }

        public FilterInfo(IDiagInfo diagInfo = null, 
            IFilterToken token = null,
            IFilterToken groupToken = null, 
            int priority = TestingContextFactory.DefaultPriority,
            int id = -1)
        {
            Token = token;
            GroupToken = groupToken;
            Priority = priority;
            DiagInfo = diagInfo;
            Id = id;
        }
    }
}
