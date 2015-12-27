namespace TestingContextCore.Implementation.Registrations
{
    using TestingContext.Interface;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Registrations.FilterRegistrations;
    using TestingContextCore.Implementation.Registrations.HighLevel;
    using TestingContextCore.Implementation.Registrations.Registration0;
    using TestingContextCore.Implementation.Registrations.Registration1;
    using TestingContextCore.Implementation.Registrations.Registration2;

    internal static class RegistrationFactory
    {
        public static IRegister GetRegistration(TokenStore store, IFilterToken groupToken, int priority)
        {
            var inner = new InnerRegistration(store, groupToken, priority);
            var innerHighLevel = new InnerHighLevelRegistration(store, groupToken, priority);
            return new Registration(store, inner, innerHighLevel);
        }

        public static IFor<T1> GetRegistration1<T1>(TokenStore store, 
            IDependency<T1> dependency, 
            IFilterToken groupToken,
            int priority)
        {
            var inner = new InnerRegistration1<T1>(store, dependency, groupToken, priority);
            var innerHighLevel = new InnerHighLevelRegistration(store, groupToken, priority, dependency);
            return new Registration1<T1>(store, inner, innerHighLevel);
        }

        public static IFor<T1,T2> GetRegistration2<T1, T2>(TokenStore store, 
            IDependency<T1> dependency1, 
            IDependency<T2> dependency2,
            IFilterToken groupToken,
            int priority)
        {
            var inner = new InnerRegistration2<T1, T2>(store, dependency1, dependency2, groupToken, priority);
            var innerHighLevel = new InnerHighLevelRegistration(store, groupToken, priority, dependency1, dependency2);
            return new Registration2<T1, T2>(store, inner, innerHighLevel);
        }
    }
}
