﻿namespace TestingContextCore.Implementation.Registrations
{
    using TestingContext.Interface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Registrations.Registration0;
    using TestingContextCore.Implementation.Registrations.Registration1;
    using TestingContextCore.Implementation.Registrations.Registration2;

    internal static class RegistrationFactory
    {
        public const int DefaultPriority = 0;

        public static IRegister GetRegistration(TokenStore store, IFilterGroup group, int priority)
        {
            var inner = new InnerRegistration(store, group, priority);
            return new Registration(store, inner);
        }

        public static IFor<T1> GetRegistration1<T1>(TokenStore store, 
            IDependency<T1> dependency, 
            IFilterGroup group,
            int priority)
        {
            var inner = new InnerRegistration1<T1>(store, dependency, group, priority);
            return new Registration1<T1>(store, inner);
        }

        public static IFor<T1,T2> GetRegistration2<T1, T2>(TokenStore store, 
            IDependency<T1> dependency1, 
            IDependency<T2> dependency2,
            IFilterGroup group,
            int priority)
        {
            var inner = new InnerRegistration2<T1, T2>(store, dependency1, dependency2, group, priority);
            return new Registration2<T1, T2>(store, inner);
        }
    }
}
