namespace TestingContextCore.Implementation.Dependencies
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;

    internal class CollectionDependency<TItem> : IDependency<IEnumerable<TItem>>
    {
        private readonly IToken<TItem> token;

        public CollectionDependency(IToken<TItem> token)
        {
            this.token = token;
        }

        public IEnumerable<TItem> GetValue(IResolutionContext context)
        {
            return context.Get(token)
                           .Distinct()
                           .Cast<IResolutionContext<TItem>>()
                           .Select(x => x.Value);
        }

        public bool TryGetValue(IResolutionContext context, out IEnumerable<TItem> value)
        {
            value = GetValue(context);
            return true;
        }

        public IToken Token => token;

        public DependencyType Type => DependencyType.Collection;
    }
}
