namespace TestingContextCore.Implementation.Filters
{
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    internal class BaseFilter
    {
        public BaseFilter(DiagInfo diagInfo, IFilter absorber)
        {
            DiagInfo = diagInfo;
            Absorber = absorber;
        }

        public IFilterToken Token { get; } = new Token();
        public DiagInfo DiagInfo { get; }
        public IFailure Absorber { get; }
    }
}
