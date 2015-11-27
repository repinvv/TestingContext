namespace TestingContextCore.PublicMembers
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.Registration;
    using TestingContextCore.Implementation.TreeOperation;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers.Exceptions;
    using static Implementation.TreeOperation.TreeOperationService;

    public class TestingContext : ITestingContext
    {
        private readonly TokenStore store;

        public TestingContext()
        {
            store = new TokenStore(this);
        }

        public IRegister Register()
        {
            return new Registration(store);
        }

        public bool FoundMatch()
        {
            var tree = GetTree(store);
            return tree.RootContext.MeetsConditions;
        }

        public IFailure GetFailure()
        {
            if (FoundMatch() || store.DisabledFilter != null)
            {
                return null;
            }

            var collect = new FailureCollect();
            GetTree(store).RootContext.ReportFailure(collect, new int[0]);
            var failure = collect.Failure;
            while (failure?.Absorber != null)
            {
                failure = failure.Absorber;
            }

            return failure;
        }

        public IEnumerable<IResolutionContext<T>> BestCandidates<T>(IToken<T> token, IFailure failure = null)
        {
            if (FoundMatch())
            {
                return All(token);
            }

            var filterToken = ((failure ?? GetFailure()) as IFilter)?.Token;
            if (filterToken == null)
            {
                throw new AlgorythmException("Failure is not found.");
            }

            store.DisableFilter(filterToken);
            return GetTree(store).RootContext.GetFromTree(token).Cast<IResolutionContext<T>>();
        }

        public IEnumerable<IResolutionContext<T>> All<T>(IToken<T> token)
        {
            store.RemoveDisabledFilter();
            return GetTree(store).RootContext.GetFromTree(token).Cast<IResolutionContext<T>>();
        }

        public void InvertFilter(IFilterToken token, int line = 0, string file = "", string member = "")
        {
            store.InvertFilter(token, DiagInfo.Create(file, line, member));
        }

        public void InvertCollectionValidity<T>(IToken<T> token, int line = 0, string file = "", string member = "")
        {
            store.InvertCollectionValidity(token, DiagInfo.Create(file, line, member));
        }

        public void InvertItemValidity<T>(IToken<T> token, int line, string file, string member)
        {
            store.InvertItemValidity(token, DiagInfo.Create(file, line, member));
        }

        public IToken<T> GetToken<T>(string name)
        {
            return store.Tokens.Get<IToken<T>>(name);
        }

        public IFilterToken GetFilterToken(string name)
        {
            return store.Tokens.Get<IFilterToken>(name);
        }

        public Storage Storage { get; } = new Storage();
    }
}
