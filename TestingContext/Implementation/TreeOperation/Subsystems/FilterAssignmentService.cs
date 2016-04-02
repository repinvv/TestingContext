namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.TreeOperation.Subsystems.NodeRelated;
    using TestingContextLimitedInterface.Tokens;
    using TestingContextLimitedInterface.UsefulExtensions;

    internal static class FilterAssignmentService
    {
        public static void AssignFilter(this TreeContext context, IFilter filter)
        {
            var finalFilter = context.GetFinalFilter(filter);
            var node = context.GetGroupNode(filter as IFilterGroup);
            if (node != null)
            {
                context.AssignFilterToNode(finalFilter, node);
                return;
            }

            var group = context.GetParentGroup(filter);
            if (group == null || context.IsFilterAbsorbed(filter, group))
            {
                context.AssignFilterToNode(finalFilter, context.GetAssignmentNode(filter));
            }
            else
            {
                group.Filters.Add(finalFilter);
            }
        }

        public static void AssignFilterToNode(this TreeContext context, IFilter filter, INode node)
        {
            node.FilterInfo.Group.Filters.Add(filter);
        }

        private class GroupOrder
        {
            private readonly Dictionary<IFilterToken, IFilter> groupTokens;
            private readonly Dictionary<IFilter, int> groupOrder;

            public GroupOrder(IEnumerable<IFilter> filters)
            {
                groupTokens = filters
                    .ToDictionary(x => x.FilterInfo.FilterToken);
                groupOrder = new Dictionary<IFilter, int>();
            }

            public int GetGroupOrder(IFilter filter)
            {
                return groupOrder.GetOrAdd(filter, () =>
                {
                    if (filter.ParentGroupToken == null)
                    {
                        return 0;
                    }
                    return GetGroupOrder(groupTokens[filter.ParentGroupToken]) + 1;
                });
            }
        }

        public static List<IFilter> GetTreeFilters(this TokenStore store)
        {
            var filters = store.FilterRegistrations
                               .Select(x => x.GetFilter()).ToList();
            var groupOrder = new GroupOrder(filters);
            return filters
                .OrderByDescending(x => x.FilterInfo.Priority)
                .ThenByDescending(x => groupOrder.GetGroupOrder(x))
                .ThenBy(x => x.FilterInfo.Id)
                .ToList();
        }

        public static void ReorderNodesForFilters(TreeContext context)
        {
            context.Filters
                .ToList()
                .ForEach(x => x.ForDependencies((dep1, dep2) => context.ReorderNodes(x, dep1, dep2)));
        }

        private static IFilter GetFinalFilter(this TreeContext context, IFilter filter)
        {
            var inversionDiag = context.IsCvFilter(filter)
                ? context.Store.CollectionInversions.SafeGet(filter.Dependencies.First().Token)
                : context.Store.FilterInversions.SafeGet(filter.FilterInfo.FilterToken);
            return inversionDiag != null ? new Inverter(filter, new FilterInfo(context.Store.NextId, inversionDiag)) : filter;
        }
    }
}
