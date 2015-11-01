namespace TestingContextCore.Implementation.Dependencies
{
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Implementation.TreeOperation.Nodes;
    using TestingContextCore.Interfaces;

    internal class SingleDependency<TSource> : IDependency<TSource>
    {
        public SingleDependency(Definition definition)
        {
            Definition = definition;
        }

        public bool TryGetValue(IResolutionContext context, NodeResolver resolver, out TSource value)
        {
            var definedcontext = context.ResolveSingle(Definition) as IResolutionContext<TSource>;
            value = definedcontext.Value;
            return true;
        }

        public Definition Definition { get; }
        public Definition ClosestParent { private get; set; }

        public bool IsCollectionDependency => false;
    }
}
