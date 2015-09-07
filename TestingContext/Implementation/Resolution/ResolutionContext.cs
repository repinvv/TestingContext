namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections.Generic;
    using TestingContextCore.Interfaces;

    internal class ResolutionContext<T> : IResolutionContext<T>, IResolution
    {
        public T Value => default(T);

        public IEnumerable<IResolution> GetChildResolution(EntityDefinition entityDefinition)
        {
            yield break;
        }
    }
}
