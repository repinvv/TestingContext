namespace TestingContextCore.Implementation.Registrations.HighLevel
{
    using System;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Registrations.FilterRegistrations;
    using TestingContextCore.Implementation.Tokens;

    internal class InnerHighLevelRegistration
    {
        private readonly TokenStore store;
        private readonly FilterGroupRegistration group;
        private readonly int priority;
        private readonly IDependency[] dependencies;

        public InnerHighLevelRegistration(TokenStore store, FilterGroupRegistration group, int priority, params IDependency[] dependencies)
        {
            this.store = store;
            this.group = group;
            this.priority = priority;
            this.dependencies = dependencies;
        }

        public IFilterToken Not(IDiagInfo diagInfo, Action<IRegister> action)
        {
            var info = new FilterInfo(id: store.NextId, diagInfo: diagInfo, token: new FilterToken(), groupToken: group?.GroupToken, priority: priority);
            var notGroup = new FilterGroupRegistration(info.Token, grp => new NotGroup(dependencies, info));
            store.RegisterFilter(notGroup, group);
            action(RegistrationFactory.GetRegistration(store, notGroup, priority));
            return info.Token;
        }

        public IFilterToken Either(IDiagInfo diagInfo, Action<IRegister>[] actions)
        {
            var info = new FilterInfo(id: store.NextId, diagInfo: diagInfo, token: new FilterToken(), groupToken: group?.GroupToken, priority: priority);
            var orGroup = new FilterGroupRegistration(info.Token, grp => new OrGroup(dependencies, info));
            store.RegisterFilter(orGroup, group);
            foreach (var action in actions)
            {
                RegisterSubgroup(action, orGroup, info);
            }

            return info.Token;
        }

        public IFilterToken Xor(IDiagInfo diagInfo, Action<IRegister> action, Action<IRegister> action2)
        {
            var info = new FilterInfo(id: store.NextId, diagInfo: diagInfo, token: new FilterToken(), groupToken: group?.GroupToken, priority: priority);
            var xorGroup = new FilterGroupRegistration(info.Token, grp => new XorGroup(dependencies, info));
            store.RegisterFilter(xorGroup, group);
            RegisterSubgroup(action, xorGroup, info);
            RegisterSubgroup(action2, xorGroup, info);
            return info.Token;
        }

        private void RegisterSubgroup(Action<IRegister> action, FilterGroupRegistration parentGroup, FilterInfo info)
        {
            var andInfo = new FilterInfo (id: store.NextId, diagInfo: info.DiagInfo, token: new FilterToken(), groupToken: info.Token, priority: info.Priority);
            var andGroup = new FilterGroupRegistration(parentGroup?.GroupToken, grp => new AndGroup(grp.NodeToken, null, andInfo));
            parentGroup.FilterRegistrations.Add(andGroup);
            action(RegistrationFactory.GetRegistration(store, andGroup, priority));
        }
    }
}
