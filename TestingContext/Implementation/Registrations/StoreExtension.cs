﻿namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Registrations.FilterRegistrations;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.PublicMembers.Exceptions;

    internal static class StoreExtension
    {
        public static void RegisterFilter(this TokenStore store, IFilterRegistration filter, FilterGroupRegistration group)
        {
            if (group != null)
            {
                group.FilterRegistrations.Add(filter);
                return;
            }

            store.Filters.Add(filter);
        }

        public static void RegisterCvFilter(this TokenStore store, IFilterRegistration filter, FilterGroupRegistration group, IFilterToken token)
        {
            store.RegisterFilter(filter, group);
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

        //public static IFilter CreateExistsFilter(
        //    IToken token,
        //    IFilterGroup group,
        //    IDiagInfo diagInfo)
        //{
        //    var dependency = new CollectionDependency(token);
        //    return new ExistsFilter(dependency, group, diagInfo);
        //}
    }
}
