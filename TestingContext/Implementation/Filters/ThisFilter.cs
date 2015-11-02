namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using ExpressionToCodeLib;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Implementation.TreeOperation.Nodes;
    using static FilterConstant;

    internal class ThisFilter<T> : IFilter
    {
        private readonly Expression<Func<IEnumerable<IResolutionContext>, bool>> filterExpression;
        private readonly Definition definition;
        private readonly Func<IEnumerable<IResolutionContext>, bool> filterFunc;

        public ThisFilter(Expression<Func<IEnumerable<IResolutionContext>, bool>> filterExpression, Definition definition)
        {
            this.filterExpression = filterExpression;
            this.definition = definition;
            filterFunc = filterExpression.Compile();
            Dependencies = new IDependency[] { new CollectionDependency<T>(definition) };
        }

        public IDependency[] Dependencies { get; }

        public bool MeetsCondition(IResolutionContext context, NodeResolver resolver, out int[] failureWeight, out IFailure failure)
        {
            var source = resolver.ResolveCollection(definition, context);
            failureWeight = EmptyArray;
            failure = this;
            return filterFunc(source);
        }

        public void Invert() { }

        #region IFailure members
        public IEnumerable<Definition> Definitions => new[] { definition };
        public string FilterString => ExpressionToCode.AnnotatedToCode(filterExpression);
        public string Key => null;
        #endregion
    }
}
