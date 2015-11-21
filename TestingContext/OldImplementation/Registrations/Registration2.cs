namespace TestingContextCore.OldImplementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using TestingContextCore.Interfaces;
    using TestingContextCore.OldImplementation.Dependencies;
    using TestingContextCore.OldImplementation.Filters;
    using TestingContextCore.OldImplementation.Providers;
    using TestingContextCore.OldImplementation.ResolutionContext;

    internal class Registration2<T1, T2> : IFor<T1, T2>
    {
        private readonly IDependency<T1> dependency1;
        private readonly IDependency<T2> dependency2;
        private readonly RegistrationStore store;
        private readonly IFilterGroup group;

        public Registration2(IDependency<T1> dependency1, IDependency<T2> dependency2, RegistrationStore store, IFilterGroup group)
        {
            this.dependency1 = dependency1;
            this.dependency2 = dependency2;
            this.store = store;
            this.group = group;
        }

        public void IsTrue(Expression<Func<T1, T2, bool>> filter, string key = null)
        {
            store.RegisterFilter(new Filter2<T1, T2>(dependency1, dependency2, filter, key, group), key);
        }

        public void Exists<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string key = null)
        {
            CreateFilter<T3>(key, x => x.Any(y => y.MeetsConditions));
            CreateProvider(key, srcFunc);
        }

        public void DoesNotExist<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string key = null)
        {
            CreateFilter<T3>(key, x => !x.Any(y => y.MeetsConditions));
            CreateProvider(key, srcFunc);
        }

        public void Each<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string key = null)
        {
            CreateFilter<T3>(key, x => x.All(y => y.MeetsConditions));
            CreateProvider(key, srcFunc);
        }

        public void Is<T3>(Func<T1, T2, T3> srcFunc, string key = null)
        {
            Exists((x, y) =>
            {
                var item = srcFunc(x, y);
                return item == null ? Enumerable.Empty<T3>() : new[] { item };
            }, key);
        }

        public void IsNot<T3>(Func<T1, T2, T3> srcFunc, string key = null)
        {
            DoesNotExist((x, y) =>
            {
                var item = srcFunc(x, y);
                return item == null ? Enumerable.Empty<T3>() : new[] { item };
            }, key);
        }

        private void CreateFilter<T3>(string key, Expression<Func<IEnumerable<IResolutionContext>, bool>> func)
        {
            store.RegisterFilter(new CollectionValidityFilter(func, Define<T3>(key)));
        }

        private void CreateProvider<T3>(string key, Func<T1, T2, IEnumerable<T3>> srcFunc)
        {
            store.RegisterProvider(Define<T2>(key), new Provider2<T1, T2, T3>(dependency1, dependency2, srcFunc));
        }
    }
}
