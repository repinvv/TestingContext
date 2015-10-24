namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using ExpressionToCodeLib;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal class ThisFilter<T> : IFilter
    {
        private readonly Expression<Func<IEnumerable<IResolutionContext<T>>, bool>> filterExpression;
        private readonly Func<IEnumerable<IResolutionContext<T>>, bool> filterFunc;

        public ThisFilter(Expression<Func<IEnumerable<IResolutionContext<T>>, bool>> filterExpression, Definition definition)
        {
            this.filterExpression = filterExpression;
            filterFunc = filterExpression.Compile();
            Dependencies = new IDependency[] { new CollectionDependency<T>(definition) };
        }

        public IDependency[] Dependencies { get; }

        public bool MeetsCondition(IResolutionContext context)
        {
            var source = (context as Resolution<T>).GetSourceCollection();
            return filterFunc(source);
        }

        public void Invert() { }

        #region IFailure members
        public string FilterString => ExpressionToCode.AnnotatedToCode(filterExpression);

        public string Key => null;

        public bool Inverted => false;
        #endregion
    }
}
