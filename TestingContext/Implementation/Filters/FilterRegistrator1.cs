namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Interfaces;
    using static Definition;

    internal class FilterRegistrator1<T1> : IFor<T1>
    {
        private readonly ContextStore store;
        private readonly IDependency<T1> dependency;

        public FilterRegistrator1(IDependency<T1> dependency, ContextStore store)
        {
            this.dependency = dependency;
            this.store = store;
        }

        public void Filter(Expression<Func<T1, bool>> filterFunc)
        {
            store.RegisterFilter(dependency.DependsOn, new Filter1<T1>(dependency, filterFunc));
        }

        public IWith<T1, T2> With<T2>(string key2) 
        {
            var dependency2 = store.Depend<T2>(dependency.DependsOn, Define<T2>(key2));
            return new FilterRegistrator2<T1, T2>(dependency, dependency2, store);
        }

        public IWith<T1, IEnumerable<IResolutionContext<T2>>> WithCollection<T2>(string key2)
        {
            var dependency2 = store.CollectionDepend<T2>(dependency.DependsOn, Define<T2>(key2));
            return new FilterRegistrator2<T1, IEnumerable<IResolutionContext<T2>>>(dependency, dependency2, store);
        }
    }
}