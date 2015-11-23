namespace TestingContextCore.Implementation.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.UsefulExtensions;

    internal class Provider2<TSource1, TSource2, T> : IProvider
    {
        private readonly IDependency<TSource1> dependency1;
        private readonly IDependency<TSource2> dependency2;
        private readonly Func<TSource1, TSource2, IEnumerable<T>> sourceFunc;

        public Provider2(IDependency<TSource1> dependency1, IDependency<TSource2> dependency2,
            Func<TSource1, TSource2, IEnumerable<T>> sourceFunc)
        {
            this.dependency1 = dependency1;
            this.dependency2 = dependency2;
            this.sourceFunc = sourceFunc;
            Dependencies = new IDependency[] { dependency1, dependency2 };
        }

        public IEnumerable<IDependency> Dependencies { get; }

        public IEnumerable<IResolutionContext> Resolve(IResolutionContext parentContext, INode node)
        {
            TSource1 sourceValue1;
            TSource2 sourceValue2;
            if (!dependency1.TryGetValue(parentContext, out sourceValue1) 
                || !dependency2.TryGetValue(parentContext, out sourceValue2))
            {
                return Enumerable.Empty<IResolutionContext>();
            }

            var source = sourceFunc(sourceValue1, sourceValue2) ?? Enumerable.Empty<T>();
            return source
                .Select(x => new ResolutionContext<T>(x, node, parentContext))
                .Cache();
        }
    }
}
