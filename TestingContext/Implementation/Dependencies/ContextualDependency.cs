namespace TestingContextCore.Implementation.Dependencies
{
    using System;
    using System.Collections.Generic;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Resolution;

    internal class CollectionDependency : IDependency<IEnumerable<IResolutionContext>>
    {
        public CollectionDependency(IToken token)
        {
            Token = token;
        }

        public IEnumerable<IResolutionContext> GetValue(IResolutionContext context)
        {
            return context.Node.Resolver.GetItems(Token, context);
        }

        public IToken Token { get; }
        public DependencyType Type => DependencyType.Collection;
        public Type SourceType => Token?.Type;
    }
}

