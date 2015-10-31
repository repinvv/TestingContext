namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using ExpressionToCodeLib;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal class ThisFilter<T> : IFilter
    {
        private readonly Expression<Func<IEnumerable<IResolutionContext<T>>, bool>> filterExpression;
        private readonly Definition definition;
        private readonly Func<IEnumerable<IResolutionContext<T>>, bool> filterFunc;

        public ThisFilter(Expression<Func<IEnumerable<IResolutionContext<T>>, bool>> filterExpression, Definition definition)
        {
            this.filterExpression = filterExpression;
            this.definition = definition;
            filterFunc = filterExpression.Compile();
            Dependencies = new IDependency[] { new CollectionDependency<T>(definition) };
        }

        public IDependency[] Dependencies { get; }

        public bool MeetsCondition(IResolutionContext context)
        {
            var source = context.ResolveDown(definition);
            return filterFunc(source as IEnumerable<IResolutionContext<T>>);
        }

        public void Invert() { }

        #region IFailure members
        public string FilterString => ExpressionToCode.AnnotatedToCode(filterExpression);

        public string Key => null;

        public bool Inverted => false;
        #endregion
    }
}
