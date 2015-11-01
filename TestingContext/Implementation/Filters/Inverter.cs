namespace TestingContextCore.Implementation.Filters
{
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Implementation.TreeOperation.Nodes;

    internal class Inverter : IFilter
    {
        private readonly IFilter filter;

        public Inverter(IFilter filter)
        {
            this.filter = filter;
        }

        public IDependency[] Dependencies => filter.Dependencies;
        public bool MeetsCondition(IResolutionContext context, NodeResolver resolver) => !filter.MeetsCondition(context, resolver);

        #region IFailure members
        public string FilterString => filter.FilterString;
        public string Key => filter.Key;
        public bool Inverted => true;
        #endregion
    }
}
