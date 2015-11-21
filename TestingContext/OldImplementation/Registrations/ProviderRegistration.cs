namespace TestingContextCore.OldImplementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using TestingContextCore.OldImplementation.Dependencies;
    using TestingContextCore.OldImplementation.Filters;
    using TestingContextCore.OldImplementation.Providers;
    using TestingContextCore.OldImplementation.ResolutionContext;

    internal class ProviderRegistration<T1> : IProvide<T1>
    {
        private readonly IDependency<T1> dependency;
        private readonly RegistrationStore store;
        private readonly IFilterGroup group;
        private readonly Definition scope;

        public ProviderRegistration(IDependency<T1> dependency, RegistrationStore store, Definition scope, IFilterGroup group)
        {
            this.dependency = dependency;
            this.store = store;
            this.group = group;
            this.scope = scope;
        }

        public void Exists<T2>(Func<T1, IEnumerable<T2>> srcFunc, string key = null)
        {
            CreateFilter<T2>(key, x => x.Any(y => y.MeetsConditions));
            CreateProvider(key, srcFunc);
        }

        public void DoesNotExist<T2>(Func<T1, IEnumerable<T2>> srcFunc, string key = null)
        {
            CreateFilter<T2>(key, x => !x.Any(y => y.MeetsConditions));
            CreateProvider(key, srcFunc);
        }

        public void Each<T2>(Func<T1, IEnumerable<T2>> srcFunc, string key = null)
        {
            CreateFilter<T2>(key, x => x.All(y => y.MeetsConditions));
            CreateProvider(key, srcFunc);
        }

        public void Is<T2>(Func<T1, T2> srcFunc, string key = null)
        {
            Exists(x =>
            {
                var item = srcFunc(x);
                return item == null ? Enumerable.Empty<T2>() : new[] { item };
            }, key);
        }

        public void IsNot<T2>(Func<T1, T2> srcFunc, string key = null)
        {
            DoesNotExist(x =>
            {
                var item = srcFunc(x);
                return item == null ? Enumerable.Empty<T2>() : new[] { item };
            }, key);
        }

        private void CreateFilter<T2>(string key, Expression<Func<IEnumerable<IResolutionContext>, bool>> func)
        {
            var dep = new CollectionValidityDependency(Definition.Define<T2>(key, scope));
            store.RegisterFilter(new Filter1<IEnumerable<IResolutionContext>>(dep, func, group, null));
        }

        private void CreateProvider<T2>(string key, Func<T1, IEnumerable<T2>> srcFunc)
        {
            store.RegisterProvider(Definition.Define<T2>(key, scope), new Provider<T1, T2>(dependency, srcFunc));
        }
    }
}