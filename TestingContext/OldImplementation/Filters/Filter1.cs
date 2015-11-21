namespace TestingContextCore.OldImplementation.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using ExpressionToCodeLib;
    using TestingContextCore.Interfaces;
    using TestingContextCore.OldImplementation.Dependencies;
    using TestingContextCore.OldImplementation.Nodes;
    using TestingContextCore.OldImplementation.ResolutionContext;

    internal class Filter1<T1> : IFilter
    {
        private readonly IDependency<T1> dependency;
        private readonly Expression<Func<T1, bool>> filterExpression;
        private readonly Func<T1, bool> filterFunc;

        public Filter1(IDependency<T1> dependency, 
            Expression<Func<T1, bool>> filterExpression, 
            IFilterGroup group,
            string key)
        {
            this.dependency = dependency;
            this.filterExpression = filterExpression;
            Key = key;
            Group = @group;
            filterFunc = filterExpression.Compile();
            Dependencies = new IDependency[] { dependency };
        }

        public IDependency[] Dependencies { get; }

        public IFilterGroup Group { get; }

        public bool MeetsCondition(IResolutionContext context, NodeResolver resolver, out int[] failureWeight, out IFailure failure)
        {
            T1 argument;
            failureWeight = FilterConstant.EmptyArray;
            failure = this;
            if (!dependency.TryGetValue(context, out argument))
            {
                return false;
            }
            return filterFunc(argument);
        }

        #region IFailure members

        public IEnumerable<string> Definitions => new[] { dependency.Definition.ToString() };

        public string FilterString => ExpressionToCode.AnnotatedToCode(filterExpression);

        public string Key { get; }

        #endregion
    }
}
