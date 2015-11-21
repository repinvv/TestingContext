namespace TestingContextCore.OldImplementation.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.OldImplementation.Dependencies;
    using TestingContextCore.OldImplementation.Nodes;
    using TestingContextCore.OldImplementation.ResolutionContext;

    internal class Provider<TSource, T> : IProvider
    {
        private readonly IDependency<TSource> dependency;
        private readonly Func<TSource, IEnumerable<T>> sourceFunc;

        public Provider(IDependency<TSource> dependency,
            Func<TSource, IEnumerable<T>> sourceFunc)
        {
            this.dependency = dependency;
            this.sourceFunc = sourceFunc;
            Dependencies = new IDependency[] { dependency };
        }

        public IDependency[] Dependencies { get; }

        public IEnumerable<IResolutionContext> Resolve(IResolutionContext parentContext, INode node)
        {
            TSource sourceValue;
            if (!dependency.TryGetValue(parentContext, out sourceValue))
            {
                return Enumerable.Empty<IResolutionContext>();
            }

            var source = sourceFunc(sourceValue) ?? Enumerable.Empty<T>();
            return source
                .Select(x => new ResolutionContext<T>(x, node, parentContext))
                .Cache();
        }
    }
}
