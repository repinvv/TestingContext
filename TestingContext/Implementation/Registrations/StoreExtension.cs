namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces.Tokens;
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

        public static void RegisterProvider(this TokenStore store, IProvider provider, IToken token)
        {
            store.PreRegister();
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

        public static void SaveToken<T>(this TokenStore store, string name, IToken<T> token)
        {
            if (store.Tokens.Get<IToken<T>>(name) != null)
            {
                throw new RegistrationException($"Definition for {token} is already registered");
            }

            token.Name = name;
            store.Tokens.Set(token, name);
        }

        public static IToken<T> GetToken<T>(this TokenStore store, string name)
        {
            var token = store.Tokens.Get<IToken<T>>(name);
            if (token == null)
            {
                throw new RegistrationException($"Entity for {typeof(T).Name} {name} is not registered.");
            }

            return token;
        }

        public static Func<IToken<T>> GetTokenFunc<T>(this TokenStore store, string name) => () => store.GetToken<T>(name);

        public static void SaveFilterToken(this TokenStore store, string name, IFilterToken token)
        {
            if (store.Tokens.Get<IFilterToken>(name) != null)
            {
                throw new RegistrationException($"Filter with name {name} is already registered");
            }

            token.Name = name;
            store.Tokens.Set(token, name);
        }

        public static IFilterToken GetFilterToken(this TokenStore store, string name)
        {
            var token = store.Tokens.Get<IFilterToken>(name);
            if (token == null)
            {
                throw new RegistrationException($"Filter with name {name} is not registered.");
            }

            return token;
        }

        public static void RegisterCvFilter(this TokenStore store, IFilter filter, IFilterGroup group)
        {
            store.RegisterFilter(filter, group);
            store.CvFilters.Add(filter);
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
