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

    internal class Filter1<T1> : IFilter
    {
        private readonly IDependency<T1> dependency;
        private readonly Expression<Func<T1, bool>> filterExpression;
        private readonly Func<T1, bool> filterFunc;
        private static readonly int[] emptyArray = new int[0];

        public Filter1(IDependency<T1> dependency, Expression<Func<T1, bool>> filterExpression, string key)
        {
            this.dependency = dependency;
            this.filterExpression = filterExpression;
            Key = key;
            filterFunc = filterExpression.Compile();
            Dependencies = new IDependency[] { dependency };
        }

        public IDependency[] Dependencies { get; }

        public bool MeetsCondition(IResolutionContext context, NodeResolver resolver, out int[] failureWeight, out IFailure failure)
        {
            T1 argument;
            failureWeight = emptyArray;
            failure = this;
            if (!dependency.TryGetValue(context, resolver, out argument))
            {
                return false;
            }
            return filterFunc(argument);
        }

        #region IFailure members

        public IEnumerable<Definition> Definitions => new[] { dependency.Definition };
        public string FilterString => ExpressionToCode.AnnotatedToCode(filterExpression);

        public string Key { get; }

        public bool Inverted => false;

        #endregion
    }
}
