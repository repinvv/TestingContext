namespace TestingContextCore.Implementation.ResolutionContext
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;
    using static Definition;

    internal class ResolutionContext<T> : IResolutionContext<T>, IResolutionContext
    {
        private readonly Definition definition;
        private readonly IResolutionContext parentContext;
        private readonly Dictionary<Definition, IResolution> resolutions = new Dictionary<Definition, IResolution>();

        public ResolutionContext(T value,
            Definition definition,
            IResolutionContext parentContext,
            List<IFilter> filters,
            List<IProvider> childProviders)
        {
            Value = value;
            this.definition = definition;
            this.parentContext = parentContext;
            TestConditions(filters, childProviders);
        }

        private void TestConditions(List<IFilter> filters, List<IProvider> childProviders)
        {
            if (filters.Any(filter => !filter.MeetsCondition(this)))
            {
                return;
            }

            foreach (var childProvider in childProviders)
            {
                var resolution = childProvider.Resolve(this);
                if (!resolution.MeetsConditions)
                {
                    MeetsConditions = false;
                    resolutions.Clear();
                    return;
                }

                resolutions.Add(childProvider.Definition, resolution);
            }

            MeetsConditions = true;
        }

        public T Value { get; }

        public bool MeetsConditions { get; private set; }

        public IEnumerable<IResolutionContext<TChild>> Resolve<TChild>(string key)
        {
            return resolutions[Define<TChild>(key)] as IEnumerable<IResolutionContext<TChild>>;
        }

        public IResolution Resolve(Definition childDef)
        {
            return resolutions[childDef];
        }

        public IResolutionContext GetContext(Definition sourceDef)
        {
            return definition.Equals(sourceDef) ? this : parentContext.GetContext(sourceDef);
        }
    }
}
