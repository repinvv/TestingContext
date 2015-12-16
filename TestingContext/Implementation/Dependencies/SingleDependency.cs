namespace TestingContextCore.Implementation.Dependencies
{
    using System;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Resolution;

    internal class SingleDependency : IDependency<IResolutionContext>
    {
        public SingleDependency(IToken token)
        {
            Token = token;
        }

        public IResolutionContext GetValue(IResolutionContext context)
        {
            return context.ResolveSingle(Token);
        }

        public IToken Token { get; }

        public DependencyType Type => DependencyType.Single;

        public Type SourceType => Token?.Type;

        public override string ToString() => $"SingleDependency {Token}";
    }
}
