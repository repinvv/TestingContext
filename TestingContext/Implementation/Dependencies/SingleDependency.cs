namespace TestingContextCore.Implementation.Dependencies
{
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal class SingleDependency<TSource> : IDependency<TSource>
    {
        private readonly Definition definition;
        private Definition closestParent;

        public SingleDependency(Definition definition, Definition dependency)
        {
            this.definition = definition;
            DependsOn = dependency;
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

        public void Validate(ContextStore store)
        {
            var node = store.GetNode(definition);
            var dependNode = store.GetNode(DependsOn);
            if (node.IsChildOf(dependNode))
            {
                return;
            }

            closestParent = store.ValidateDependency(node, dependNode);
        }

        public Definition DependsOn { get; }

        public bool IsCollectionDependency => false;
    }
}
