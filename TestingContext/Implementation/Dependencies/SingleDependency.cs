namespace TestingContextCore.Implementation.Dependencies
{
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Resolution;

    internal class SingleDependency : IDependency<IResolutionContext>
    {
        public SingleDependency(IToken token)
        {
            Token = token;
        }

        public bool TryGetValue(IResolutionContext context, out IResolutionContext value)
        {
            value = context.ResolveSingle(Token);
            return true;
        }

        public IToken Token { get; }

        public DependencyType Type => DependencyType.Single;
    }
}
