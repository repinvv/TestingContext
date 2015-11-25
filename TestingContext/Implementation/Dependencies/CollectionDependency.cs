namespace TestingContextCore.Implementation.Dependencies
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;

    internal class CollectionDependency<TItem> : IDependency<IEnumerable<TItem>>
    {
        private readonly LazyToken<TItem> token;

        public CollectionDependency(LazyToken<TItem> token)
        {
            this.token = token;
        }

        public IEnumerable<TItem> GetValue(IResolutionContext context)
        {
            return context.Node.Resolver.GetFitItems(Token, context)
                           .Cast<IResolutionContext<TItem>>()
                           .Select(x => x.Value);
        }

        public bool TryGetValue(IResolutionContext context, out IEnumerable<TItem> value)
        {
            value = GetValue(context);
            return true;
        }

        public IToken Token => token.Value;

        public DependencyType Type => DependencyType.Collection;
    }
}
