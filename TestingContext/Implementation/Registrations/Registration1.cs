namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;
    using static Definition;

    internal class Registration1<T1> : IFor<T1>
    {
        private readonly IDependency<T1> dependency;
        private readonly RegistrationStore store;
        private readonly IFilterGroup group;

        public Registration1(IDependency<T1> dependency, RegistrationStore store, IFilterGroup group)
        {
            this.dependency = dependency;
            this.store = store;
            this.group = group;
        }

        public void IsTrue(Expression<Func<T1, bool>> filter, string key = null)
        {
           store.RegisterFilter(new Filter1<T1>(dependency, filter, key, group), key);
        }

        public IFor<T1, T2> For<T2>(string key)
        {
            var second = new SingleDependency<T2>(Define<T2>(key));
            return new Registration2<T1, T2>(dependency, second, store, group);
        }

        public IFor<T1, IEnumerable<T2>> ForAll<T2>(string key)
        {
            var second = new CollectionDependency<T2>(Define<T2>(key));
            return new Registration2<T1, IEnumerable<T2>>(dependency, second, store, group);
        }

        public void Exists<T2>(string key, Func<T1, IEnumerable<T2>> srcFunc)
        {
            CreateFilter<T2>(key, x => x.Any(y => y.MeetsConditions));
            CreateProvider(key, srcFunc);
        }

        public void DoesNotExist<T2>(string key, Func<T1, IEnumerable<T2>> srcFunc)
        {
            CreateFilter<T2>(key, x => !x.Any(y => y.MeetsConditions));
            CreateProvider(key, srcFunc);
        }

        public void Each<T2>(string key, Func<T1, IEnumerable<T2>> srcFunc)
        {
            CreateFilter<T2>(key, x => x.All(y => y.MeetsConditions));
            CreateProvider(key, srcFunc);
        }

        public void Satisfies<T2>(string key, Func<T1, T2> srcFunc)
        {
            Exists(key, x =>
            {
                var item = srcFunc(x);
                return item == null ? Enumerable.Empty<T2>() : new[] { item };
            });
        }

        public void DoesNotSatisfy<T2>(string key, Func<T1, T2> srcFunc)
        {
            DoesNotExist(key, x =>
            {
                var item = srcFunc(x);
                return item == null ? Enumerable.Empty<T2>() : new[] { item };
            });
        }

        private void CreateFilter<T2>(string key, Expression<Func<IEnumerable<IResolutionContext>, bool>> func)
        {
            store.RegisterFilter(new CollectionValidityFilter(func, Define<T2>(key)));
        }

        private void CreateProvider<T2>(string key, Func<T1, IEnumerable<T2>> srcFunc)
        {
            store.RegisterProvider(Define<T2>(key), new Provider<T1, T2>(dependency, srcFunc));
        }
    }
}
