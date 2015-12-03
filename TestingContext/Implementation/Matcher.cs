namespace TestingContextCore.Implementation
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.PublicMembers.Exceptions;

    internal class Matcher : IMatcher
    {
        private readonly IResolutionContext rootContext;
        private readonly TokenStore store;
        private DisablableFilter failedFilter;

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
            return GetFailedFilter();
        }

        private DisablableFilter GetFailedFilter()
        {
            if (FoundMatch() || failedFilter != null)
            {
                return failedFilter;
            }

            var collect = new FailureCollect();
            rootContext.ReportFailure(collect, new int[0]);
            var failure = collect.Failure;
            while (failure?.Absorber != null)
            {
                failure = failure.Absorber;
            }

            return failedFilter = failure as DisablableFilter;
        }

        public IEnumerable<IResolutionContext<T>> BestCandidates<T>(IToken<T> token)
        {
            if (FoundMatch())
            {
                return All(token);
            }
            var failure = GetFailedFilter();
            if (failure == null)
            {
                throw new AlgorythmException("Failure is not found.");
            }

            failure.Disable();
            rootContext.Evaluate();
            return All(token);
        }

        public IEnumerable<IResolutionContext<T>> BestCandidates<T>(string name) => BestCandidates(store.GetToken<T>(name));

        public IEnumerable<IResolutionContext<T>> All<T>(IToken<T> token) => rootContext.GetFromTree(token).Cast<IResolutionContext<T>>();

        public IEnumerable<IResolutionContext<T>> All<T>(string name) => All(store.GetToken<T>(name));
    }
}
