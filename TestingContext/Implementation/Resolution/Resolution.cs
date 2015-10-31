namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Implementation.TreeOperation;
    using TestingContextCore.Implementation.TreeOperation.Nodes;

    internal class Resolution<T> : IEnumerable<IResolutionContext>
    {
        private readonly Definition ownDefinition;
        private readonly IResolutionContext parent;
        private readonly IEnumerable<ResolutionContext<T>> resolvedSource;

        public Resolution(Definition ownDefinition,
            IResolutionContext parent,
            IEnumerable<T> source,
            Node node)
        {
            this.ownDefinition = ownDefinition;
            this.parent = parent;
            resolvedSource = source
                .Select(x => new ResolutionContext<T>(x, ownDefinition, node, parent))
                .Cache();
        }

        private IEnumerable<IResolutionContext> ResolutionContent => resolvedSource
            .Where(x => x.MeetsConditions)
            .Select(item => item as IResolutionContext);

        IEnumerator<IResolutionContext> IEnumerable<IResolutionContext>.GetEnumerator() => ResolutionContent.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ResolutionContent.GetEnumerator();
    }
}
