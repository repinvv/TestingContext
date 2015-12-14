namespace TestingContextCore.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::TestingContext.Interface;
    using global::TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Resolution;

    internal class Matcher : IMatcher
    {
        private readonly IResolutionContext rootContext;
        private readonly TokenStore store;

        public Matcher(IResolutionContext rootContext, TokenStore store)
        {
            this.rootContext = rootContext;
            this.store = store;
        }

        public bool FoundMatch()
        {
            return rootContext.MeetsConditions;
        }

        public IFailure GetFailure()
        {
            return rootContext.FailingFilter;
        }

        public IEnumerable<IResolutionContext<T>> All<T>(IToken<T> token) 
            => rootContext.GetFromTree(token).Cast<IResolutionContext<T>>();

        public IEnumerable<IResolutionContext<T>> All<T>(string name) 
            => All(store.GetToken<T>(name));

        public IEnumerable<Tuple<IFailure, T>> Candidates<T>(string name) 
            => Candidates(store.GetToken<T>(name));

        public IEnumerable<Tuple<IFailure, T>> Candidates<T>(IToken<T> token)
            => rootContext.Node.Resolver.GetItems(token, rootContext)
                          .Select(x => new Tuple<IFailure, T>(x.FailingFilter,
                                                              (x as IResolutionContext<T>).Value));
    }
}
