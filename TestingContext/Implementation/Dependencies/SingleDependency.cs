namespace TestingContextCore.Implementation.Dependencies
{
    using TestingContextCore.Implementation.ResolutionContext;

    internal class SingleDependency<TSource> : IDependency<TSource>
    {
        public SingleDependency(Definition definition)
        {
            Definition = definition;
        }

        public bool TryGetValue(IResolutionContext context, out TSource value)
        {
            value = default(TSource);
            return true;
        }

        public Definition Definition { get; }
        public Definition ClosestParent { private get; set; }

        public bool IsCollectionDependency => false;
    }
}
