namespace TestingContextCore.Implementation.Registrations.Registration0
{
    using System;
    using System.Collections.Generic;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Registrations.Registration1;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.PublicMembers;

    internal class InnerRegistration 
    {
        private readonly TokenStore store;
        private readonly IFilterGroup group;
        private readonly int priority;

        public InnerRegistration(TokenStore store, IFilterGroup group, int priority)
        {
            this.store = store;
            this.group = group;
            this.priority = priority;
        }

        #region groups
        public IFilterToken Not(Action<IRegister> action, string file, int line, string member)
        {
            var notGroup = new NotGroup(DiagInfo.Create(file, line, member));
            store.RegisterFilter(notGroup, group);
            RegisterSubgroup(action, notGroup);
            return notGroup.Token;
        }

        public IFilterToken Or(Action<IRegister> action,
            Action<IRegister> action2,
            Action<IRegister> action3,
            Action<IRegister> action4,
            Action<IRegister> action5,
            string file,
            int line,
            string member)
        {
            var orGroup = new OrGroup(DiagInfo.Create(file, line, member));
            store.RegisterFilter(orGroup, group);
            RegisterSubgroup(action, orGroup);
            RegisterSubgroup(action2, orGroup);
            RegisterSubgroup(action3, orGroup);
            RegisterSubgroup(action4, orGroup);
            RegisterSubgroup(action5, orGroup);
            return orGroup.Token;
        }

        public IFilterToken Xor(Action<IRegister> action, Action<IRegister> action2, string file, int line, string member)
        {
            var xorGroup = new XorGroup(DiagInfo.Create(file, line, member));
            store.RegisterFilter(xorGroup, group);
            RegisterSubgroup(action, xorGroup);
            RegisterSubgroup(action2, xorGroup);
            return xorGroup.Token;
        }

        private void RegisterSubgroup(Action<IRegister> action, IFilterGroup parentGroup)
        {
            if (action == null)
            {
                return;
            }

            var andGroup = new AndGroup { Id = store.NextId };
            parentGroup.Filters.Add(andGroup);
            action(RegistrationFactory.GetRegistration(store, andGroup, priority));
        }
        #endregion

        public IFor<T> For<T>(IHaveToken<T> haveToken)
        {
            return RegistrationFactory.GetRegistration1(store, new SingleValueDependency<T>(haveToken), group, priority);
        }

        public IFor<IEnumerable<T>> ForCollection<T>(IHaveToken<T> haveToken)
        {
            return RegistrationFactory.GetRegistration1(store, new CollectionValueDependency<T>(haveToken), group, priority);
        }
        
        public IHaveToken<T> Exists<T>(Func<IEnumerable<T>> srcFunc, IDiagInfo diagInfo)
        {
            var dependency = new SingleValueDependency<Root>(new HaveToken<Root>(store.RootToken));
            return new InnerRegistration1<Root>(store, dependency, group, priority)
                .Declare(x => srcFunc(), diagInfo)
                .Exists(diagInfo);
        }
    }
}
