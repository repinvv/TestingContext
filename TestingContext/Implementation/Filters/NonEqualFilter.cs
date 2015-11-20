namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;
    using static TestingContextCore.Implementation.Dependencies.DependencyType;
    using static FilterConstant;

    internal class NonEqualFilter : IFilter
    {
        private readonly Definition definition1;
        private readonly Definition definition2;

        public NonEqualFilter(Definition definition1, Definition definition2)
        {
            this.definition1 = definition1;
            this.definition2 = definition2;
            Dependencies = new IDependency[] { new DummyDependency(definition1, Single), new DummyDependency(definition2, Single) };
            Definitions = new[] { definition1.ToString(), definition2.ToString() };
            FilterString = "(a, b) => a != b";
        }

        #region IFilter
        public IDependency[] Dependencies { get; }

        public IFilterGroup Group => null;

        public bool MeetsCondition(IResolutionContext context, NodeResolver resolver, out int[] failureWeight, out IFailure failure)
        {
            failureWeight = EmptyArray;
            failure = this;
            var context1 = context.ResolveSingle(definition1);
            var context2 = context.ResolveSingle(definition2);
            return !Equals(context1, context2);
        }
        #endregion

        #region IFailure members
        public IEnumerable<string> Definitions { get; }

        public string FilterString { get; }

        public string Key => null;
        #endregion
    }
}
