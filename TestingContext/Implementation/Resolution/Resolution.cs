namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ResolutionContext;
    using TestingContextCore.Implementation.TreeOperation.Nodes;

    internal class Resolution<T> : IEnumerable<IResolutionContext>
    {
        private readonly IEnumerable<ResolutionContext<T>> resolvedSource;

        public Resolution(IResolutionContext parent,
            IEnumerable<T> source,
            INode node)
        {
            resolvedSource = source
                .Select(x => new ResolutionContext<T>(x, node, parent))
                .Cache();
        }

        private IEnumerable<IResolutionContext> ResolutionContent => resolvedSource
            .Select(item => item as IResolutionContext);

        IEnumerator<IResolutionContext> IEnumerable<IResolutionContext>.GetEnumerator() => ResolutionContent.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ResolutionContent.GetEnumerator();
    }
}
