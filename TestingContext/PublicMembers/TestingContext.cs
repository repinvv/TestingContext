namespace TestingContextCore.PublicMembers
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Inversion;
    using TestingContextCore.Interfaces.Register;
    using TestingContextCore.Interfaces.Tokens;
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

        public IInversion Inversion { get; }

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

        public IEnumerable<IResolutionContext<T>> BestCandidates<T>(string name, IFailure failure)
            => BestCandidates(store.GetToken<T>(name));

        public IEnumerable<IResolutionContext<T>> All<T>(IToken<T> token)
        {
            store.RemoveDisabledFilter();
            return GetTree(store).RootContext.GetFromTree(token).Cast<IResolutionContext<T>>();
        }

        public IEnumerable<IResolutionContext<T>> All<T>(string name) => All(store.GetToken<T>(name));

        public Storage Storage { get; } = new Storage();
    }
}
