﻿namespace TestingContextCore.Implementation.Registration
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
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

        public IToken<Root> RootToken { get; } = new Token<Root>();

        public Dictionary<IToken, IProvider> Providers { get; } = new Dictionary<IToken, IProvider>();

        public List<IFilter> Filters { get; } = new List<IFilter>();

        public Tree Tree { get; set; }

        public Dictionary<IToken, DiagInfo> ItemInversions { get; } =new Dictionary<IToken, DiagInfo>();

        public Dictionary<IToken, DiagInfo> CollectionInversions { get; } = new Dictionary<IToken, DiagInfo>();

        public Dictionary<IToken, DiagInfo> FilterInversions { get; } = new Dictionary<IToken, DiagInfo>();
    }
}