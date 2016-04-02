namespace TestingContextCore.Implementation.Registrations
{
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Registrations.FilterRegistrations;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.PublicMembers.Exceptions;
    using TestingContextLimitedInterface.Diag;
    using TestingContextLimitedInterface.Tokens;

    internal static class StoreExtension
    {
        public static void RegisterFilter(this TokenStore store, FilterRegistration filter)
        {
            store.FilterRegistrations.Add(filter);
        }

        public static void RegisterCvFilter(this TokenStore store, FilterRegistration filter, IFilterToken token)
        {
            store.RegisterFilter(filter);
            store.CvFilters.Add(token);
        }

        public static void RegisterProvider(this TokenStore store, IProvider provider, IToken token)
        {
            store.Providers.Add(token, provider);
        }

        public static void SaveToken<T>(this TokenStore store, IDiagInfo diagInfo, string name, IToken<T> token)
        {
            if (store.GetToken<T>(name) != null)
            {
                throw new RegistrationException($"Definition for {token} is already registered", diagInfo);
            }

            if (token == null)
            {
                throw new RegistrationException($"Attempted to register null as token for {typeof(T).Name} \"{name}\"", diagInfo);
            }
            
            store.Tokens.Set(token, name);
            token.Name = name;
        }

        public static IHaveToken<T> GetHaveToken<T>(this TokenStore store, IDiagInfo diagInfo, string name)
        {
            var token = store.GetToken<T>(name);
            if (token != null)
            {
                return new HaveToken<T>(token);
            }

            return new LazyHaveToken<T>(() => store.GetToken<T>(name), name, diagInfo);
        }

        public static IToken<T> GetToken<T>(this TokenStore store, string name) 
            => store.Tokens.Get<IToken<T>>(name);
    }
}
