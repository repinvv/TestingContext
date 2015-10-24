namespace TestingContextCore.Implementation.ResolutionContext
{
    using System.Collections.Generic;
    using TestingContextCore.Interfaces;

    internal class ResolutionContext<T> : IResolutionContext<T>, IResolutionContext
    {
        private readonly IResolutionContext parent;

        public ResolutionContext(T value,
            Definition ownDefinition,
            IResolutionContext parent)
        {
            this.parent = parent;
            Value = value;
        }

        public bool MeetsConditions => true;

        public T Value { get; }

        public IEnumerable<IResolutionContext<T2>> Get<T2>(string key)
        {
            yield break;
        }
    }
}
