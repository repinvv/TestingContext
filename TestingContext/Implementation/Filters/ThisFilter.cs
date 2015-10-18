namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using ExpressionToCodeLib;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal class ThisFilter<T> : IFilter
    {
        private readonly Expression<Func<IEnumerable<IResolutionContext<T>>, bool>> filterExpression;
        private readonly Func<IEnumerable<IResolutionContext<T>>, bool> filterFunc;

        public ThisFilter(Expression<Func<IEnumerable<IResolutionContext<T>>, bool>> filterExpression)
        {
            this.filterExpression = filterExpression;
            filterFunc = filterExpression.Compile();
        }

        public bool MeetsCondition(IResolutionContext context)
        {
            var source = (context as Resolution<T>).GetSourceCollection();
            return filterFunc(source);
        }

        public void Invert() { }

        #region IFailure members
        public string FailureString => ExpressionToCode.AnnotatedToCode(filterExpression);

        public string Key => null;

        public bool Inverted => false;
        #endregion
    }
}
