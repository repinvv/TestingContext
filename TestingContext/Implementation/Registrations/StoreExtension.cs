namespace TestingContextCore.Implementation.Registrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Registrations.LoopDetection;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.PublicMembers.Exceptions;

    internal static class StoreExtension
    {
        public static void RegisterFilter(this TokenStore store, IFilter filter, IFilterGroup group)
        {
            filter.Id = store.NextId;
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

        public static void InvertFilter(this TokenStore store, IFilterToken token, IDiagInfo diagInfo)
        {
            store.FilterInversions.Add(token, diagInfo);
        }

        public static void InvertCollectionValidity(this TokenStore store, IToken token, IDiagInfo diagInfo)
        {
            store.CollectionInversions.Add(token, diagInfo);
        }

        public static void InvertItemValidity(this TokenStore store, IToken token, IDiagInfo diagInfo)
        {
            store.ItemInversions.Add(token, diagInfo);
        }

        public static void SaveToken<T>(this TokenStore store, string name, IToken<T> token, IDiagInfo diagInfo)
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

        public static IHaveToken<T> GetHaveToken<T>(this TokenStore store, string name, string file, int line, string member)
        {
            var token = store.GetToken<T>(name);
            if (token != null)
            {
                return new HaveToken<T>(token);
            }

            return new LazyHaveToken<T>(() => store.GetToken<T>(name), name);
        }

        public static IToken<T> GetToken<T>(this TokenStore store, string name) 
            => store.Tokens.Get<IToken<T>>(name);

        public static IFilter CreateCvFilter(Expression<Func<IEnumerable<IResolutionContext>, bool>> filterExpr,
            IToken token, IDiagInfo diagInfo)
        {
            var cv = new CollectionDependency(token);
            return new Filter1<IEnumerable<IResolutionContext>>(cv, filterExpr.Compile(), diagInfo);
        }

        public static IFilter CreateExistsFilter(
            IToken token,
            IDiagInfo diagInfo)
        {
            var dependency = new CollectionDependency(token);
            return new ExistsFilter(dependency, diagInfo);
        }
    }
}
