namespace TestingContextCore.Implementation.Dependencies
{
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Exceptions;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Interfaces;

    internal class SingleDependency<TSource> : IDependency<TSource>
        where TSource : class
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
            var resolved = context.GetContext(DependsOn) as IResolutionContext<TSource>;
            if (resolved == null)
            {
                throw new ResolutionException($"Could not resolve the value of {DependsOn}, " +
                                              "this most likely means no item meets the specified conditions");
            }

            return resolved.Value;
        }

        public void Validate(ContextStore store)
        {
            var node = store.GetNode(definition);
            var dependNode = store.GetNode(DependsOn);
            if (node.IsChildOf(dependNode))
            {
                return;
            }

            if (dependNode.IsChildOf(node))
            {
                throw new ResolutionException($"{definition} is registered use {DependsOn} as singular dependency, " +
                                              $"while {DependsOn} is registered as a child of {definition}. " +
                                              $"This is a prohibited scenario, singular dependency can only reference parent, " +
                                              $"or other cranches.");
            }

            closestParent = store.ValidateDependency(node, dependNode);
        }

        public Definition DependsOn { get; }

        public bool IsCollectionDependency => false;
    }
}
