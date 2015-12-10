namespace TestingContextCore.Implementation.Dependencies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Resolution;

    internal class CollectionValueDependency<TItem> : IDependency<IEnumerable<TItem>>
    {
        private readonly IHaveToken<TItem> haveToken;

        public CollectionValueDependency(IHaveToken<TItem> haveToken)
        {
            this.haveToken = haveToken;
        }

        public IEnumerable<TItem> GetValue(IResolutionContext context)
        {
            return context.GetFromTree(Token)
                          .Cast<IResolutionContext<TItem>>()
                          .Select(x => x.Value);
        }

        public IToken Token => haveToken.Token;

        public DependencyType Type => DependencyType.Collection;
        public Type SourceType => typeof(TItem);
    }
}
