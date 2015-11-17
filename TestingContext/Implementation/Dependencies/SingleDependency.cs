namespace TestingContextCore.Implementation.Dependencies
{
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;
    using static DependencyType;

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

        public DependencyType Type => Single;
    }
}
