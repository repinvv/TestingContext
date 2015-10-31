namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Linq.Expressions;
    using ExpressionToCodeLib;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.ResolutionContext;

    internal class Filter1<T1> : IFilter
    {
        private readonly IDependency<T1> dependency;
        private readonly Expression<Func<T1, bool>> filterExpression;
        private readonly Func<T1, bool> filterFunc;

        public Filter1(IDependency<T1> dependency, Expression<Func<T1, bool>> filterExpression, string key)
        {
            this.dependency = dependency;
            this.filterExpression = filterExpression;
            Key = key;
            filterFunc = filterExpression.Compile();
            Dependencies = new IDependency[] { dependency };
        }

        public IDependency[] Dependencies { get; }

        public bool MeetsCondition(IResolutionContext context)
        {
            T1 argument;
            dependency.TryGetValue(context, out argument);
            return filterFunc(argument);
        }

        #region IFailure members
        public string FilterString => ExpressionToCode.AnnotatedToCode(filterExpression);

        public string Key { get; }

        public bool Inverted => false;

        #endregion
    }
}
