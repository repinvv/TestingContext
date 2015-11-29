namespace TestingContextCore.Implementation.Dependencies
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces.Tokens;

    internal class CollectionDependency : IDependency<IEnumerable<IResolutionContext>>
    {
        public CollectionDependency(IToken token)
        {
            Token = token;
        }

        public bool TryGetValue(IResolutionContext context, out IEnumerable<IResolutionContext> value)
        {
            value = context.Node.Resolver.GetAllItems(Token, context);
            return true;
        }
        public IToken Token { get; }
        public DependencyType Type => DependencyType.Collection;
    }
}

