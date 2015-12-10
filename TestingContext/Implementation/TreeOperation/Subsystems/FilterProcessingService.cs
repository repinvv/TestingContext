namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;

    internal static class FilterProcessingService
    {
        public static void ProcessFilterGroup(IFilterGroup filterGroup, List<IFilter> freeFilters, TokenStore store, Tree tree)
        {
            if (filterGroup == null)
            {
                return;
            }

            var groupFilters = filterGroup.Filters.ToList();
            filterGroup.Filters.Clear();
            var cvFilters = groupFilters.Where(store.IsCvFilter).ToList();

            foreach (var filter in groupFilters)
            {
                ProcessFilterGroup(filter as IFilterGroup, freeFilters, store, tree);
                var targetList = cvFilters
                    .Where(x=>x!= filter)
                    .Any(x => FilterIsAbsorbed(filter, tree.Nodes[x.Dependencies.First().Token], tree))
                    ? freeFilters
                    : filterGroup.Filters;
                AddFilter(filter, targetList, store);
            }
        }

        private static bool FilterIsAbsorbed(IFilter filter, INode cvNode, Tree tree)
        {
            return filter.Dependencies.Any(x => DependencyIsAbsorbed(x, cvNode, tree));
        }

        private static bool DependencyIsAbsorbed(IDependency dependency, INode cvNode, Tree tree)
        {
            var node = dependency.GetDependencyNode(tree);
            return node == cvNode || node.IsChildOf(cvNode);
        }

        public static void AddFilter(IFilter filter, List<IFilter> filters, TokenStore store)
        {
            if (!filter.Dependencies.Any())
            {
                return;
            }

            var inversionDiag = store.IsCvFilter(filter)
                ? store.CollectionInversions.SafeGet(filter.Dependencies.First().Token)
                : store.FilterInversions.SafeGet(filter.Token);
            filters.Add(inversionDiag != null ? new NotGroup(filter, inversionDiag) : filter);
        }
    }
}
