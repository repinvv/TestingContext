namespace TestingContextCore.Implementation.Filters
{
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Tokens;

    internal class BaseFilter
    {
        public BaseFilter(IDiagInfo diagInfo, IFilter absorber)
        {
            DiagInfo = diagInfo;
            Absorber = absorber;
        }

        public IFilterToken Token { get; } = new Token();
        public IDiagInfo DiagInfo { get; }
        public IFilter Absorber { get; }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
