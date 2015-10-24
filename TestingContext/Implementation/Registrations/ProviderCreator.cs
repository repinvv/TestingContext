namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Interfaces;
    using static Definition;

    internal abstract class ProviderCreator<T1> : ICreateProvider<T1>
    {
        private readonly Definition definition;
        private readonly RegistrationStore store;

        protected ProviderCreator(Definition definition, RegistrationStore store)
        {
            this.definition = definition;
            this.store = store;
        }

        public void Exists<T2>(string key, Func<T1, IEnumerable<T2>> srcFunc)
        {
            store.RegisterFilter(new ThisFilter<T2>(x => x.Any(y => y.MeetsConditions), Define<T2>(key)), null);
            CreateProvider(key, srcFunc);
        }

        public void DoesNotExist<T2>(string key, Func<T1, IEnumerable<T2>> srcFunc)
        {
            store.RegisterFilter(new ThisFilter<T2>(x => !x.Any(y => y.MeetsConditions), Define<T2>(key)), null);
            CreateProvider(key, srcFunc);
        }

        public void Each<T2>(string key, Func<T1, IEnumerable<T2>> srcFunc)
        {
            store.RegisterFilter(new ThisFilter<T2>(x => x.All(y => y.MeetsConditions), Define<T2>(key)), null);
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

        private void CreateProvider<T2>(string key, Func<T1, IEnumerable<T2>> srcFunc)
        {
            var dep = new SingleDependency<T1>(definition);
            store.RegisterProvider(definition, new Provider<T1, T2>(dep, srcFunc, store));
        }
    }
}
