namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.PublicMembers;
    using TestingContextCore.PublicMembers.Exceptions;

    internal static class StoreExtension
    {
        public static void RegisterFilter(this TokenStore store, IFilter filter, IFilterGroup group)
        {
            store.PreRegister();
            if (group != null)
            {
                group.Filters.Add(filter);
                return;
            }

            store.Filters.Add(filter);
        }

        public static void RegisterProvider(this TokenStore store, IProvider provider, IToken token, IFilterGroup group)
        {
            store.RegisterFilter(provider.CollectionValidityFilter, group);
            store.CvFilters.Add(provider.CollectionValidityFilter);
            store.Providers.Add(token, provider);
        }

        public static void InvertFilter(this TokenStore store, IFilterToken token, DiagInfo diagInfo)
        {
            store.PreRegister();
            store.FilterInversions.Add(token, diagInfo);
        }

        public static void InvertCollectionValidity(this TokenStore store, IToken token, DiagInfo diagInfo)
        {
            store.PreRegister();
            store.CollectionInversions.Add(token, diagInfo);
        }

        public static void InvertItemValidity(this TokenStore store, IToken token, DiagInfo diagInfo)
        {
            store.PreRegister();
            store.ItemInversions.Add(token, diagInfo);
        }

        public static void DisableFilter(this TokenStore store, IFilterToken token)
        {
            store.PreRegister();
            store.DisabledFilter = token;
        }

        public static void RemoveDisabledFilter(this TokenStore store)
        {
            if (store.DisabledFilter == null)
            {
                return;
            }

            store.PreRegister();
            store.DisabledFilter = null;
        }

        private static void PreRegister(this TokenStore store)
        {
            store.Tree = null;
        }

        public static void SaveToken<T>(this TokenStore store, string name, IToken<T> token, string file, int line, string member)
        {
            if (store.Tokens.Get<IToken<T>>(name) != null)
            {
                throw new RegistrationException($"Definition for {token} is already registered", DiagInfo.Create(file, line, member));
            }

            token.Name = name;
            store.Tokens.Set(token, name);
        }

        public static IHaveToken<T> GetHaveToken<T>(this TokenStore store, string name, string file, int line, string member)
        {
            var token = store.GetToken<T>(name);
            if (token != null)
            {
                return new HaveToken<T>(token);
            }

            return new LazyHaveToken<T>(() => store.Tokens.Get<IToken<T>>(name), DiagInfo.Create(file, line, member));
        }

        public static IToken<T> GetToken<T>(this TokenStore store, string name)
        {
            return store.Tokens.Get<IToken<T>>(name);
        }

        public static IFilter CreateCvFilter(Func<IEnumerable<IResolutionContext>, bool> filterExpr,
            IToken token,
            IFilter absorber,
            DiagInfo diagInfo)
        {
            var cv = new CollectionDependency(token);
            return new Filter1<IEnumerable<IResolutionContext>>(cv, filterExpr, diagInfo, absorber);
        }
    }
}
