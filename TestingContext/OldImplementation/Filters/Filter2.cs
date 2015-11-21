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

    internal class Filter2<T1, T2> : IFilter
    {
        private readonly IDependency<T1> dependency1;
        private readonly IDependency<T2> dependency2;
        private readonly Expression<Func<T1, T2, bool>> filterExpression;
        private readonly Func<T1, T2, bool> filterFunc;

        public Filter2(IDependency<T1> dependency1, 
            IDependency<T2> dependency2,
            Expression<Func<T1, T2, bool>> filterExpression, 
            string key, 
            IFilterGroup group)
        {
            this.dependency1 = dependency1;
            this.dependency2 = dependency2;
            this.filterExpression = filterExpression;
            Key = key;
            Group = @group;
            filterFunc = filterExpression.Compile();
            Dependencies = new IDependency[] { dependency1, dependency2 };
        }

        #region IFilter
        public IDependency[] Dependencies { get; }

        public IFilterGroup Group { get; }

        public bool MeetsCondition(IResolutionContext context, NodeResolver resolver, out int[] failureWeight, out IFailure failure)
        {
            T1 argument1;
            T2 argument2;
            failureWeight = FilterConstant.EmptyArray;
            failure = this;

            if (!dependency1.TryGetValue(context, out argument1) || !dependency2.TryGetValue(context, out argument2))
            {
                return false;
            }

            return filterFunc(argument1, argument2);
        }
        #endregion

        #region IFailure members

        public IEnumerable<string> Definitions => new[] { dependency1.Definition.ToString(), dependency2.Definition.ToString() };

        public string FilterString => ExpressionToCode.AnnotatedToCode(filterExpression);

        public string Key { get; }

        #endregion
    }
}
