namespace TestingContextCore.Implementation
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Filters;

    internal class ContextCore
    {
        public IEnumerable<ResolutionContext<T>> Resolve<T>(string key)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterFilter(IFilter filter, params EntityDefinition[] definitions)
        { }
    }
}
