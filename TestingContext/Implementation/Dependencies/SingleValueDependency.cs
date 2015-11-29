namespace TestingContextCore.Implementation.Dependencies
{
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;

    internal class SingleValueDependency<TItem> : IDependency<TItem>
    {
        private readonly IHaveToken<TItem> haveToken;

        public SingleValueDependency(IHaveToken<TItem> haveToken)
        {
            this.haveToken = haveToken;
        }

        public bool TryGetValue(IResolutionContext context, out TItem value)
        {
            var definedcontext = context.ResolveSingle(Token) as IResolutionContext<TItem>;
            value = definedcontext.Value;
            return true;
        }

        public IToken Token => haveToken.Token;
        public DependencyType Type => DependencyType.Single;
    }
}
