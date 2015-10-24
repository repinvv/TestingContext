namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Logging;
    using TestingContextCore.Implementation.ResolutionContext;
    using Interfaces;

    internal class Resolution<T> : IResolution, IResolutionContext
    {
        private readonly Definition ownDefinition;
        private readonly IResolutionContext parent;
        private readonly IEnumerable<ResolutionContext<T>> resolvedSource;
        private bool? meetsCondition;

        public Resolution(Definition ownDefinition,
            IResolutionContext parent,
            IEnumerable<T> source)
        {
            this.ownDefinition = ownDefinition;
            this.parent = parent;
            resolvedSource = source
                .Select(x => new ResolutionContext<T>(x, ownDefinition, parent))
                .Cache();
        }

        private IEnumerable<IResolutionContext> ResolutionContent => resolvedSource
            .Where(x => x.MeetsConditions)
            .Select(item => item as IResolutionContext);

        IEnumerator<IResolutionContext> IEnumerable<IResolutionContext>.GetEnumerator() => ResolutionContent.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ResolutionContent.GetEnumerator();

        public bool MeetsConditions => true;

        public IEnumerable<IResolutionContext<T>> GetSourceCollection()
        {
            return resolvedSource;
        }

        public void ReportFailure(FailureCollect collect, int[] startingWeight)
        { }
    }
}
