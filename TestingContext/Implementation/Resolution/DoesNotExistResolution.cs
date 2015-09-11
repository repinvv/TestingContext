namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.CachingEnumerable;
    using TestingContextCore.Interfaces;

    internal class DoesNotExistResolution<T> : Resolution<T>
    {
        private readonly IEnumerable<IResolutionContext<T>> source;

        public DoesNotExistResolution(IEnumerable<IResolutionContext<T>> source, Definition definition)
            : base(definition)
        {
            //this.source = source.Where(x => x.MeetsConditions).Cache();
        }

        public IEnumerator<IResolutionContext<T>> GetEnumerator() => source.GetEnumerator();

        public override bool MeetsConditions => !source.Any();
    }
}
