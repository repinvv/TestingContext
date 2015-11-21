namespace TestingContextCore.Implementation.Dependencies
{
    using TestingContextCore.Implementation;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;

    internal class SingleDependency<TItem> : IDependency<TItem>
    {
        private readonly LazyToken<TItem> token;

        public SingleDependency(LazyToken<TItem> token)
        {
            this.token = token;
        }

        public bool TryGetValue(IResolutionContext context, out TItem value)
        {
            var definedcontext = context.ResolveSingle(Token) as IResolutionContext<TItem>;
            value = definedcontext.Value;
            return true;
        }

        public IToken Token => token.Value;
        public DependencyType Type => DependencyType.Single;
    }
}
