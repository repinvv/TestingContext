namespace TestingContextCore.Implementation.Dependencies
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces.Tokens;

    internal class CollectionValidityDependency : IDependency<IEnumerable<IResolutionContext>>
    {
        public CollectionValidityDependency(IToken token)
        {
            Token = token;
        }

        public bool TryGetValue(IResolutionContext context, out IEnumerable<IResolutionContext> value)
        {
            value = null; // todo context.Node.Resolver.ResolveCollection(Token, context).Distinct();
            return true;
        }
        public IToken Token { get; }
        public DependencyType Type => DependencyType.CollectionValidity;
    }
}

