namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.CachingEnumerable;
    using TestingContextCore.Interfaces;

    internal class ExistsResolution<T> : Resolution<T>
    {
        private readonly IEnumerable<IResolutionContext<T>> source;

        public ExistsResolution(IEnumerable<IResolutionContext<T>> source, Definition definition)
            : base(definition)
        {
            //this.source = source.Where(x => x.MeetsConditions).Cache();
        }

        public override bool MeetsConditions => source.Any();
    }
}
