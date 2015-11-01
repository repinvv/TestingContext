namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Linq.Expressions;
    using ExpressionToCodeLib;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Implementation.TreeOperation.Nodes;

    internal class Filter2<T1, T2> : IFilter
    {
        private readonly IDependency<T1> dependency1;
        private readonly IDependency<T2> dependency2;
        private readonly Expression<Func<T1, T2, bool>> filterExpression;
        private readonly Func<T1, T2, bool> filterFunc;

        public Filter2(IDependency<T1> dependency1, IDependency<T2> dependency2, Expression<Func<T1, T2, bool>> filterExpression, string key)
        {
            this.dependency1 = dependency1;
            this.dependency2 = dependency2;
            this.filterExpression = filterExpression;
            Key = key;
            filterFunc = filterExpression.Compile();
            Dependencies = new IDependency[] { dependency1, dependency2 };
        }

        public IDependency[] Dependencies { get; }

        public bool MeetsCondition(IResolutionContext context, NodeResolver resolver)
        {
            T1 argument1;
            T2 argument2;
            if (!dependency1.TryGetValue(context, resolver, out argument1) || !dependency2.TryGetValue(context, resolver, out argument2))
            {
                return false;
            }

            return filterFunc(argument1, argument2);
        }
        
        #region IFailure members
        public string FilterString => ExpressionToCode.AnnotatedToCode(filterExpression);

        public string Key { get; }

        public bool Inverted => false;

        #endregion
    }
}
