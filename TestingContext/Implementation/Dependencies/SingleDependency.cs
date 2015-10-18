namespace TestingContextCore.Implementation.Dependencies
{
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal class SingleDependency<TSource> : IDependency<TSource>
    {
        public SingleDependency(Definition definition)
        {
            DependsOn = definition;
        }

        public TSource GetValue(IResolutionContext context)
        {
            var resolved = context.ResolveSingle(DependsOn, closestParent) as IResolutionContext<TSource>;
            return resolved != null ? resolved.Value : default(TSource);
        }

        public bool TryGetValue(IResolutionContext context, out TSource value)
        {
            var resolved = context.ResolveSingle(DependsOn, closestParent) as IResolutionContext<TSource>;
            if (resolved == null)
            {
                value = default(TSource);
                return default(TSource) == null;
            }

            value = resolved.Value;
            return true;
        }

        public Definition DependsOn { get; }

        public bool IsCollectionDependency => false;
    }
}
