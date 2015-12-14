namespace TestingContextCore.Implementation.Registrations.HighLevel
{
    using System;
    using global::TestingContext.Interface;
    using global::TestingContext.LimitedInterface.Diag;
    using global::TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters.Groups;

    internal class InnerHighLevelRegistration
    {
        private readonly TokenStore store;
        private readonly IFilterGroup group;
        private readonly int priority;
        private readonly IDependency[] dependencies;

        public InnerHighLevelRegistration(TokenStore store, IFilterGroup group, int priority, params IDependency[] dependencies)
        {
            this.store = store;
            this.group = group;
            this.priority = priority;
            this.dependencies = dependencies;
        }

        public IFilterToken Not(IDiagInfo diagInfo, Action<IRegister> action)
        {
            var notGroup = new NotGroup(dependencies, group, diagInfo);
            store.RegisterFilter(notGroup, group);
            action(RegistrationFactory.GetRegistration(store, notGroup, priority));
            return notGroup.Token;
        }

        public IFilterToken Either(IDiagInfo diagInfo, Action<IRegister>[] actions)
        {
            var orGroup = new OrGroup(dependencies, group, diagInfo);
            store.RegisterFilter(orGroup, group);
            foreach (var action in actions)
            {
                RegisterSubgroup(action, orGroup);
            }

            return orGroup.Token;
        }

        public IFilterToken Xor(IDiagInfo diagInfo, Action<IRegister> action, Action<IRegister> action2)
        {
            var xorGroup = new XorGroup(dependencies, group, diagInfo);
            store.RegisterFilter(xorGroup, group);
            RegisterSubgroup(action, xorGroup);
            RegisterSubgroup(action2, xorGroup);
            return xorGroup.Token;
        }

        private void RegisterSubgroup(Action<IRegister> action, IFilterGroup parentGroup)
        {
            var andGroup = new AndGroup(parentGroup.GroupToken, null, parentGroup) { Id = store.NextId };
            parentGroup.Filters.Add(andGroup);
            action(RegistrationFactory.GetRegistration(store, andGroup, priority));
        }
    }
}
