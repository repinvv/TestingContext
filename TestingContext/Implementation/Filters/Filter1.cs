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
            this.Key = key;
            filterFunc = filterExpression.Compile();
            Definitions = new []{ dependency.DependsOn };
        }

        public bool IsCollectionFilter => dependency.IsCollectionDependency;
        public Definition[] Definitions { get; }

        public bool MeetsCondition(IResolutionContext context)
        {
            return filterFunc(dependency.GetValue(context)) ^ Inverted;
        }

        public void Invert()
        {
            Inverted = !Inverted;
        }

        #region IFailure members
        public string FailureString => ExpressionToCode.AnnotatedToCode(filterExpression);

        public string Key { get; }

        public bool Inverted { get; private set; }
        #endregion
    }
}
