﻿namespace TestingContextCore.Implementation.Dependencies
{
    using System;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Resolution;

    internal class SingleValueDependency<TItem> : IDependency<TItem>
    {
        private readonly IHaveToken<TItem> haveToken;

        public SingleValueDependency(IHaveToken<TItem> haveToken)
        {
            this.haveToken = haveToken;
        }

        public TItem GetValue(IResolutionContext context)
        {
            var definedcontext = context.ResolveSingle(Token) as IResolutionContext<TItem>;
            return definedcontext.Value;
        }

        public IToken Token => haveToken.Token;
        public DependencyType Type => DependencyType.Single;
        public Type SourceType => typeof(TItem);
    }
}
