namespace TestingContextCore.Implementation.Dependencies
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces.Tokens;

    internal class ContextualDependency : IDependency<IEnumerable<IResolutionContext>>
    {
        public ContextualDependency(IToken token, DependencyType dependencyType)
        {
            Token = token;
            Type = dependencyType;
        }

        public bool TryGetValue(IResolutionContext context, out IEnumerable<IResolutionContext> value)
        {
            value = context.Node.Resolver.GetAllItems(Token, context);
            return true;
        }
        public IToken Token { get; }
        public DependencyType Type { get; }
    }
}

