namespace TestingContextCore.PublicMembers
{
    using System.Collections.Generic;
    using System.Linq;
    using global::TestingContext.Interface;
    using global::TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.PublicMembers.Exceptions;
    using static Implementation.TreeOperation.TreeOperationService;

    public class TestingContext : ITestingContext
    {
        private readonly TokenStore store;

        public TestingContext()
        {
            store = new TokenStore(this);
            Inversion = new Inversion(store);
        }

        public IRegister Register()
        {
            return RegistrationFactory.GetRegistration(store);
        }

        public IHaveToken<T> GetToken<T>(string name, string file, int line, string member)
        {
            return store.GetHaveToken<T>(name, file, line, member);
        }

        public void SetToken<T>(string name, IHaveToken<T> haveToken, string file, int line, string member)
        {
            store.SaveToken(name, haveToken.Token, file, line, member);
        }

        public IInversion Inversion { get; }

        public bool FoundMatch()
        {
            var tree = GetTree(store);
            return tree.RootContext.MeetsConditions;
        }

        private IFilter GetFailedFilter()
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

        public IFailure GetFailure() => GetFailedFilter();

        public IEnumerable<IResolutionContext<T>> BestCandidates<T>(IToken<T> token, IFailure failure = null)
        {
            if (FoundMatch())
            {
                return All(token);
            }

            var filterToken = ((failure as IFilter) ?? GetFailedFilter())?.Token;
            if (filterToken == null)
            {
                throw new AlgorythmException("Failure is not found.");
            }

            store.DisableFilter(filterToken);
            return GetTree(store).RootContext.GetFromTree(token).Cast<IResolutionContext<T>>();
        }

        public IEnumerable<IResolutionContext<T>> BestCandidates<T>(string name, IFailure failure)
            => BestCandidates(store.GetToken<T>(name));

        public IEnumerable<IResolutionContext<T>> All<T>(IToken<T> token)
        {
            store.RemoveDisabledFilter();
            return GetTree(store).RootContext.GetFromTree(token).Cast<IResolutionContext<T>>();
        }

        public IEnumerable<IResolutionContext<T>> All<T>(string name) => All(store.GetToken<T>(name));
        
        public IStorage Storage { get; } = new Storage();
    }
}
