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
    using static Implementation.TreeOperation.TreeOperationService;

    public class Registration<T> : IRegister<T>
    {
        private readonly IFilterGroup group;
        private readonly Definition scope;
        internal readonly RegistrationStore Store;

        internal Registration()
        {
            Store = new RegistrationStore();
            scope = Store.RootDefinition;
        }

        internal Registration(RegistrationStore store, IFilterGroup group = null, Definition scope = null)
        {
            Store = store;
            this.group = group;
            this.scope = scope;
        }

        public IFor<T1> For<T1>(string key)
        {
            return new Registration1<T1>(new SingleDependency<T1>(Define<T1>(key, scope)), Store, group);
        }

        public IFor<IEnumerable<T1>> ForAll<T1>(string key)
        {
            return new Registration1<IEnumerable<T1>>(new CollectionDependency<T1>(Define<T1>(key, scope)), Store, group);
        }

        public IEnumerable<IResolutionContext<T1>> Get<T1>(string key)
        {
            var tree = GetTree(Store);
            if (!tree.RootContext.MeetsConditions)
            {
                return Enumerable.Empty<IResolutionContext<T1>>();
            }
            var all = tree.Root
                          .Resolver
                          .ResolveCollection(Define<T1>(key, Store.RootDefinition), tree.RootContext)
                          .Where(x => x.MeetsConditions)
                          .Distinct()
                          .Cast<IResolutionContext<T1>>();
            return all;
        }
    }
}
