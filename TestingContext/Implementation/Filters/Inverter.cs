namespace TestingContextCore.Implementation.Filters
{
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.ResolutionContext;

    internal class Inverter : IFilter
    {
        private readonly IFilter filter;

        public Inverter(IFilter filter)
        {
            this.filter = filter;
        }

        public IDependency[] Dependencies => filter.Dependencies;
        public bool MeetsCondition(IResolutionContext context) => !filter.MeetsCondition(context);

        #region IFailure members
        public string FilterString => filter.FilterString;
        public string Key => filter.Key;
        public bool Inverted => true;
        #endregion
    }
}
