namespace TestingContextCore.Implementation.Registration
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.Implementation.TreeOperation;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    internal class TokenStore
    {
        public TokenStore(ITestingContext context)
        {
            Context = context;
        }

        public ITestingContext Context { get; }

        public Storage Tokens { get; } = new Storage();

        public IToken RootToken { get; } = new Token();

        public List<IFilter> Filters { get; } = new List<IFilter>();

        public Tree Tree { get; set; }
    }
}
