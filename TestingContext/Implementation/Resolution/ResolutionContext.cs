namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections.Generic;
    using System.Linq;
    using Filters;
    using Sources;
    using TestingContextCore.Interfaces;

    internal class ResolutionContext<T> : IResolutionContext<T>, IResolve
    {
        private readonly IEnumerable<IFilter> filters;
        private List<ISource> sources;
        private T value;
        private Dictionary<EntityDefinition, IResolution> resolutions = new Dictionary<EntityDefinition, IResolution>();

        public ResolutionContext(T value, IEnumerable<IFilter> filters, List<ISource> sources)
        {
            this.value = value;
            this.filters = filters;
            this.sources = sources;
        }

        public T Value => default(T);

        public bool MeetsConditions
        {
            get
            {
                var meets = filters.All(x => x.MeetsCondition(this));
                if (!meets)
                {
                    return false;
                }

                foreach (var source in sources)
                {
                    var resolution = source.Resolve(value);
                    if (!resolution.MeetsConditions)
                    {
                        return false;
                    }
                       
                    resolutions.Add(source.EntityDefinition, resolution); 
                }

                return true;
            }
        }
    }
}
