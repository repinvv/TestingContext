namespace TestingContextCore.PublicMembers
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Registration;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;

    public class TestingContext : ITestingContext
    {
        private readonly TokenStore store = new TokenStore();
        private readonly Storage tokens = new Storage();

        //public bool FoundMatch()
        //{
        //    //return GetTree(store).RootContext.MeetsConditions;
        //}

        //public IFailure GetFailure()
        //{
        //    //if (FoundMatch())
        //    //{
        //    //    return null;
        //    //}

        //    //var collect = new FailureCollect();
        //    //GetTree(Store).RootContext.ReportFailure(collect, new int[0]);
        //    //return collect.Failure;
        //}

        //public void InvertFilter(string key)
        //{
        //    //Store.RegisterFilterInversion(key);
        //}

        //public void InvertCollectionValidity<T>(string key)
        //{
        //    //Store.RegisterCollectionValidityInversion(Define<T>(key, Store.RootDefinition));
        //}

        //public void InvertItemValidity<T>(string key)
        //{
        //    //Store.RegisterItemValidityInversion(Define<T>(key, Store.RootDefinition));
        //}

        public IRegister Register()
        {
            return new Registration(store);
        }

        public bool FoundMatch()
        {
            return false;
        }

        public IFailure GetFailure()
        {
            return null;
        }

        public IEnumerable<IResolutionContext<T>> All<T>(IToken<T> token)
        {
            yield break;
        }

        public void InvertFilter(IToken token)
        { }

        public void InvertCollectionValidity(IToken token)
        { }

        public void InvertItemValidity(IToken token)
        { }

        public IToken<T> GetToken<T>(string name)
        {
            return tokens.Get<IToken<T>>(name);
        }

        public IToken GetToken(string name)
        {
            return tokens.Get<IToken>(name);
        }

        public IToken GetFilterToken(string name)
        {
            return null;
        }

        public Storage Storage { get; } = new Storage();
    }
}
