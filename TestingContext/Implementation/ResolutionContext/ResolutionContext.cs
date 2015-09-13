namespace TestingContextCore.Implementation.ResolutionContext
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;

    internal class ResolutionContext<T> : IResolutionContext<T>, IResolutionContext
    {
        private readonly IEnumerable<IFilter> filters;
        private readonly List<IProvider> dependentSources;
        private readonly T value;
        private readonly IResolutionContext parentContext;
        private Dictionary<Definition, IResolution> resolutions = new Dictionary<Definition, IResolution>();

        public ResolutionContext(T value, IResolutionContext parentContext, IEnumerable<IFilter> filters, List<IProvider> dependentSources)
        {
            this.value = value;
            this.parentContext = parentContext;
            this.filters = filters;
            this.dependentSources = dependentSources;
        }

        public T Value => default(T);

        public IEnumerable<IResolutionContext<TChild>> Resolve<TChild>(string key)
        {
            yield break;
        }

        public IResolution Resolve(Definition definition)
        {
            return null;
        }

        public bool MeetsConditions
        {
            get
            {
                return false;
                //var meets = filters.All(x => x.MeetsCondition(this));
                //if (!meets)
                //{
                //    return false;
                //}

                //foreach (var source in dependentSources)
                //{
                //    var resolution = source.Resolve(this);
                //    if (!resolution.MeetsConditions)
                //    {
                //        return false;
                //    }

                //    resolutions.Add(source.Definition, resolution); 
                //}

                //return true;
            }
        }
    }
}
