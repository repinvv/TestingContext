namespace TestingContextCore.Implementation.Dependencies
{
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Resolution;

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
