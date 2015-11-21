namespace TestingContextCore.OldImplementation.Dependencies
{
    using TestingContextCore.OldImplementation.ResolutionContext;

    internal class SingleDependency<TSource> : IDependency<TSource>
    {
        public SingleDependency(Definition definition)
        {
            Definition = definition;
        }

        public bool TryGetValue(IResolutionContext context, out TSource value)
        {
            var definedcontext = context.ResolveSingle(Definition) as IResolutionContext<TSource>;
            value = definedcontext.Value;
            return true;
        }

        public Definition Definition { get; }

        public DependencyType Type => DependencyType.Single;
    }
}
