namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Linq.Expressions;
    using ExpressionToCodeLib;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.ResolutionContext;

    internal class ThisFilter<T1> : IFilter
    {
        private readonly IDependency<T1> dependency;
        private readonly Expression<Func<T1, bool>> filterExpression;
        private readonly Func<T1, bool> filterFunc;

        public ThisFilter(IDependency<T1> dependency, Expression<Func<T1, bool>> filterExpression)
        {
            this.dependency = dependency;
            this.filterExpression = filterExpression;
            filterFunc = filterExpression.Compile();
            Definitions = new[] { dependency.DependsOn };
        }

        public bool IsCollectionFilter => dependency.IsCollectionDependency;
        public Definition[] Definitions { get; }

        public bool MeetsCondition(IResolutionContext context)
        {
            return filterFunc(dependency.GetThisValue(context));
        }

        public string FailureString => ExpressionToCode.AnnotatedToCode(filterExpression);
    }
}
