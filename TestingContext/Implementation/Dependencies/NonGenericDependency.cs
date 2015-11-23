namespace TestingContextCore.Implementation.Dependencies
{
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces.Tokens;

    internal class NonGenericDependency :IDependency<IResolutionContext>
    {
        public NonGenericDependency(IToken token)
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
