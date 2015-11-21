namespace TestingContextCore.Implementation.Dependencies
{
    using TestingContextCore.Implementation;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;

    internal class SingleDependency<TItem> : IDependency<TItem>
    {
        private readonly IToken<TItem> token;

        public SingleDependency(IToken<TItem> token)
        {
            this.token = token;
        }

        public bool TryGetValue(IResolutionContext context, out TItem value)
        {
            var definedcontext = context.ResolveSingle(token) as IResolutionContext<TItem>;
            value = definedcontext.Value;
            return true;
        }

        public IToken Token => token;
        public DependencyType Type => DependencyType.Single;
    }
}
