﻿namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Linq.Expressions;
    using ExpressionToCodeLib;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.ResolutionContext;

    internal class Filter2<T1, T2> : IFilter
    {
        private readonly IDependency<T1> dependency1;
        private readonly IDependency<T2> dependency2;
        private readonly Expression<Func<T1, T2, bool>> filterExpression;
        private readonly Func<T1, T2, bool> filterFunc;

        public Filter2(IDependency<T1> dependency1, IDependency<T2> dependency2, Expression<Func<T1, T2, bool>> filterExpression, string key)
        {
            Definitions = new[] { dependency1.DependsOn, dependency2.DependsOn };
            this.dependency1 = dependency1;
            this.dependency2 = dependency2;
            this.filterExpression = filterExpression;
            this.Key = key;
            this.filterFunc = filterExpression.Compile();
        }

        public Definition[] Definitions { get; }

        public bool IsCollectionFilter => dependency1.IsCollectionDependency;

        public bool MeetsCondition(IResolutionContext context)
        {
            var argument1 = dependency1.GetValue(context);
            T2 argument2;
            if (!dependency2.TryGetValue(context, out argument2)) return false;
            var result = filterFunc(argument1, argument2);
            return result ^ Inverted;
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
