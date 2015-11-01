﻿namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using ExpressionToCodeLib;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Implementation.TreeOperation.Nodes;
    using TestingContextCore.Interfaces;

    internal class ThisFilter<T> : IFilter
    {
        private readonly Expression<Func<IEnumerable<IResolutionContext>, bool>> filterExpression;
        private readonly Definition definition;
        private readonly Func<IEnumerable<IResolutionContext>, bool> filterFunc;

        public ThisFilter(Expression<Func<IEnumerable<IResolutionContext>, bool>> filterExpression, Definition definition)
        {
            this.filterExpression = filterExpression;
            this.definition = definition;
            filterFunc = filterExpression.Compile();
            Dependencies = new IDependency[] { new CollectionDependency<T>(definition) };
        }

        public IDependency[] Dependencies { get; }

        public bool MeetsCondition(IResolutionContext context, NodeResolver resolver)
        {
            var source = resolver.ResolveCollection(definition, context);
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
