namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registration;
    using TestingContextCore.UsefulExtensions;

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
            var cvNodes = groupFilters.Where(x => x.IsCvFilter()).Select(x => tree.Nodes[x.Dependencies.First().Token]).ToList();
            foreach (var filter in groupFilters)
            {
                ProcessFilterGroup(filter as IFilterGroup, freeFilters, store, tree);
                var targetList = cvNodes.Any(x => FilterIsAbsorbed(filter, x, tree))
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
            return (node == cvNode && dependency.Type != DependencyType.CollectionValidity)
                   || node.IsChildOf(cvNode);
        }

        public static void AddFilter(IFilter filter, List<IFilter> filters, TokenStore store)
        {
            var inversionDiag = filter.IsCvFilter()
                ? store.CollectionInversions.SafeGet(filter.Dependencies.First().Token)
                : store.FilterInversions.SafeGet(filter.Token);
            filters.Add(inversionDiag == null ? filter : new Inverter(filter, inversionDiag));
        }
    }
}
