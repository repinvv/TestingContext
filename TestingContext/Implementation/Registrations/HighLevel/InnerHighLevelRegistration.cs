namespace TestingContextCore.Implementation.Registrations.HighLevel
{
    using System;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Registrations.FilterRegistrations;
    using TestingContextInterface;
    using TestingContextLimitedInterface.Diag;
    using TestingContextLimitedInterface.Tokens;

    internal class InnerHighLevelRegistration
    {
        private readonly TokenStore store;
        private readonly IFilterToken groupToken;
        private readonly int priority;
        private readonly IDependency[] dependencies;

        public InnerHighLevelRegistration(TokenStore store, IFilterToken groupToken, int priority, params IDependency[] dependencies)
        {
            this.store = store;
            this.groupToken = groupToken;
            this.priority = priority;
            this.dependencies = dependencies;
        }

        public IFilterToken Not(IDiagInfo diagInfo, Action<IRegister> action)
        {
            var info = new FilterInfo(store.NextId, diagInfo, groupToken, priority);
            var notGroup = new FilterRegistration(() => new NotGroup(dependencies, info));
            store.RegisterFilter(notGroup);
            action(RegistrationFactory.GetRegistration(store, info.FilterToken, priority));
            return info.FilterToken;
        }

        public IFilterToken And(IDiagInfo diagInfo, Action<IRegister> action)
        {
            var info = new FilterInfo(store.NextId, diagInfo, groupToken, priority);
            var andGroup = new FilterRegistration(() => new AndGroup(dependencies, info));
            store.RegisterFilter(andGroup);
            action(RegistrationFactory.GetRegistration(store, info.FilterToken, priority));
            return info.FilterToken;
        }

        public IFilterToken Or(IDiagInfo diagInfo, Action<IRegister>[] actions)
        {
            var info = new FilterInfo(store.NextId, diagInfo, groupToken, priority);
            var orGroup = new FilterRegistration(() => new OrGroup(dependencies, info));
            store.RegisterFilter(orGroup);
            foreach (var action in actions)
            {
                RegisterSubgroup(action, info);
            }
            
            return info.FilterToken;
        }

        public IFilterToken Xor(IDiagInfo diagInfo, Action<IRegister> action, Action<IRegister> action2)
        {
            var info = new FilterInfo(store.NextId, diagInfo, groupToken, priority);
            var xorGroup = new FilterRegistration(() => new XorGroup(dependencies, info));
            store.RegisterFilter(xorGroup);
            RegisterSubgroup(action, info);
            RegisterSubgroup(action2, info);
            return info.FilterToken;
        }

        private void RegisterSubgroup(Action<IRegister> action, FilterInfo parentInfo)
        {
            var andInfo = new FilterInfo (store.NextId, parentInfo.DiagInfo, parentInfo.FilterToken, parentInfo.Priority);
            var andGroup = new FilterRegistration(() => new AndGroup(null, andInfo));
            store.RegisterFilter(andGroup);
            action(RegistrationFactory.GetRegistration(store, andInfo.FilterToken, priority));
        }
    }
}
