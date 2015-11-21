namespace TestingContextCore.OldImplementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Interfaces;
    using TestingContextCore.OldImplementation.Dependencies;
    using TestingContextCore.OldImplementation.Filters;
    using TestingContextCore.OldImplementation.TreeOperation;

    internal class Registration<T> : ProviderRegistration<T>, IForContext<T>
    {
        private readonly IFilterGroup group;
        private readonly Definition scope;
        private readonly RegistrationStore store;

        internal Registration(RegistrationStore store, Definition scope, IFilterGroup group = null)
            : base (new SingleDependency<T>(scope),store, scope, group)
        {
            this.store = store;
            this.group = group;
            this.scope = scope;
        }

        public void ScopeBy<T1>(Action<IForContext<T1>> action, string key = null)
        {
            var definition = Definition.Define<T1>(key, scope);
            action(new Registration<T1>(store, definition, group));
        }

        public IFor<T1> For<T1>(string key)
        {
            return new Registration1<T1>(new SingleDependency<T1>(Definition.Define<T1>(key, scope)), store, group, scope);
        }

        public IFor<IEnumerable<T1>> ForAll<T1>(string key)
        {
            return new Registration1<IEnumerable<T1>>(new CollectionDependency<T1>(Definition.Define<T1>(key, scope)), store, group, scope);
        }

        public IEnumerable<IResolutionContext<T1>> Get<T1>(string key)
        {
            var tree = TreeOperationService.GetTree(store);
            if (!tree.RootContext.MeetsConditions)
            {
                return Enumerable.Empty<IResolutionContext<T1>>();
            }
            var all = tree.Root
                          .Resolver
                          .ResolveCollection(Definition.Define<T1>(key, store.RootDefinition), tree.RootContext)
                          .Where(x => x.MeetsConditions)
                          .Distinct()
                          .Cast<IResolutionContext<T1>>();
            return all;
        }
    }
}
