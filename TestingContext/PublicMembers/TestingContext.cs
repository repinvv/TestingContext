namespace TestingContextCore.PublicMembers
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.Registration;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;
    using static TestingContextCore.Implementation.TreeOperation.TreeOperationService;

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
            if (FoundMatch())
            {
                return null;
            }

            var collect = new FailureCollect();
            GetTree(store).RootContext.ReportFailure(collect, new int[0]);
            return collect.Failure;
        }

        public IEnumerable<IResolutionContext<T>> All<T>(IToken<T> token)
        {
            var tree = GetTree(store);
            return tree.RootContext
                       .GetFromTree(token)
                       .Cast<IResolutionContext<T>>();
        }

        public void InvertFilter(IToken token, int line, string file, string member)
        {
            store.InvertFilter(token, new DiagInfo(file, line, member));
        }

        public void InvertCollectionValidity(IToken token, int line, string file, string member)
        {
            store.InvertCollectionValidity(token, new DiagInfo(file, line, member));
        }

        public void InvertItemValidity(IToken token, int line, string file, string member)
        {
            store.InvertItemValidity(token, new DiagInfo(file, line, member));
        }

        public IToken<T> GetToken<T>(string name)
        {
            return store.Tokens.Get<IToken<T>>(name);
        }

        public IToken GetToken(string name)
        {
            return store.Tokens.Get<IToken>(name);
        }

        public Storage Storage { get; } = new Storage();
    }
}
