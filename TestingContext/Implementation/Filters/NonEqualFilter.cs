namespace TestingContextCore.Implementation.Filters
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Implementation.TreeOperation.Nodes;
    using static FilterConstant;

    internal class NonEqualFilter : IFilter
    {
        private readonly Definition definition1;
        private readonly Definition definition2;

        public NonEqualFilter(Definition definition1, Definition definition2)
        {
            this.definition1 = definition1;
            this.definition2 = definition2;
            Dependencies = new IDependency[] { new DummyDependency(definition1, false), new DummyDependency(definition2, false) };
            Definitions = new[] { definition1, definition2 };
            FilterString = "(a, b) => a != b";
        }

        public IEnumerable<Definition> Definitions { get; }
        public string FilterString { get; }
        public string Key => null;
        public IDependency[] Dependencies { get; }

        public bool MeetsCondition(IResolutionContext context, NodeResolver resolver, out int[] failureWeight, out IFailure failure)
        {
            failureWeight = EmptyArray;
            failure = this;
            var context1 = context.ResolveSingle(definition1);
            var context2 = context.ResolveSingle(definition2);
            return !Equals(context1, context2);
        }
    }
}
