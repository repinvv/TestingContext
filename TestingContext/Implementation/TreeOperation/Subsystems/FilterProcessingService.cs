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
        public static void ProcessFilterGroup(IFilterGroup filterGroup, List<IFilter> freeFilters, TokenStore store)
        {
            if (filterGroup == null)
            {
                return;
            }

            var groupFilters = filterGroup.Filters.ToList();
            filterGroup.Filters.Clear();
            var cvNodes = groupFilters.Where(x => x.IsCvFilter()).Select(x => store.Tree.Nodes[x.Dependencies.First().Token]).ToList();
            foreach (var filter in groupFilters)
            {
                ProcessFilterGroup(filter as IFilterGroup, freeFilters, store);
                var targetList = cvNodes.Any(x => FilterIsAbsorbed(filter, x, store.Tree))
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
            if (!filter.Dependencies.Any() || store.DisabledFilter == filter.Token)
            {
                return;
            }

            var inversionDiag = filter.IsCvFilter()
                ? store.CollectionInversions.SafeGet(filter.Dependencies.First().Token)
                : store.FilterInversions.SafeGet(filter.Token);
            filters.Add(inversionDiag == null ? filter : new Inverter(filter, inversionDiag));
        }
    }
}
