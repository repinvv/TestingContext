namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.UsefulExtensions;

    internal static class FilterProcessingService
    {
        public static void ProcessFilterGroup(IFilterGroup filterGroup, List<IFilter> freeFilters, TokenStore store)
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
                ProcessFilterGroup(filter as IFilterGroup, freeFilters, store);
                var targetList = cvFilters
                    .Where(x=>x!= filter)
                    .Any(x => FilterIsAbsorbed(filter, store.Tree.Nodes[x.Dependencies.First().Token], store.Tree))
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
