namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using ExpressionToCodeLib;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;
    using static TestingContextCore.Implementation.Dependencies.DependencyType;
    using static FilterConstant;

    internal class CollectionValidityFilter : IFilter
    {
        private readonly Expression<Func<IEnumerable<IResolutionContext>, bool>> filterExpression;
        private readonly Definition definition;
        private readonly Func<IEnumerable<IResolutionContext>, bool> filterFunc;

        public CollectionValidityFilter(Expression<Func<IEnumerable<IResolutionContext>, bool>> filterExpression, Definition definition, IFilterGroup group)
        {
            this.filterExpression = filterExpression;
            this.definition = definition;
            filterFunc = filterExpression.Compile();
            Dependencies = new IDependency[] { new DummyDependency(definition, CollectionValidity) };
            Group = group;
        }

        #region IFilter
        public IDependency[] Dependencies { get; }

        public IFilterGroup Group { get; }

        public bool MeetsCondition(IResolutionContext context, NodeResolver resolver, out int[] failureWeight, out IFailure failure)
        {
            var source = resolver.ResolveCollection(definition, context);
            failureWeight = EmptyArray;
            failure = this;
            return filterFunc(source);
        }
        #endregion

        public void Invert() { }

        #region IFailure members
        public IEnumerable<string> Definitions => new[] { definition.ToString() };

        public string FilterString => ExpressionToCode.AnnotatedToCode(filterExpression);

        public string Key => null;
        #endregion
    }
}
