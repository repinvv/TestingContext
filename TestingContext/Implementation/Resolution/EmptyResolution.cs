namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.ResolutionContext;

    internal class EmptyResolution : IResolution
    {
        public IEnumerator<IResolutionContext> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield break;
        }

        public bool MeetsConditions => false;
    }
}
