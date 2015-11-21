namespace TestingContextCore.OldImplementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.OldImplementation.Filters;
    using TestingContextCore.OldImplementation.Nodes;
    using TestingContextCore.OldImplementation.Registrations;

    internal static class FilterAssignmentService
    {
        public static INode GetAssignmentNode(Tree tree, IHaveDependencies have)
        {
            return have.Dependencies
                       .Select(x => x.GetDependencyNode(tree))
                       .OrderByDescending(x => x.GetParentalChain().Count)
                       .First();
        }

        public static void AssignFilter(Tree tree, IFilter filter, RegistrationStore store)
        {
            var node = GetAssignmentNode(tree, filter);
            AddFilterToGroup(node.Filters.Group, filter, tree, store);
        }

        public static void CheckAndAssignFilter(Tree tree, IFilter filter, RegistrationStore store)
        {
            NonEqualFilteringService.AssignNonEqualFilters(tree, filter, store);
            AssignFilter(tree, filter, store);
        }

        public static void ReorderCheckAndAssignFilter(Tree tree, IFilter filter, RegistrationStore store)
        {
            NodeReorderingService.ReorderNodes(tree, filter);
            CheckAndAssignFilter(tree, filter, store);
        }

        public static void AssignFilters(Tree tree, RegistrationStore store)
        {
            var regular = new List<IFilter>();
            var cv = new List<IFilter>();
            var groups = new Dictionary<IFilterGroup, List<IFilter>>();
            
            foreach (var filter in store.Filters)
            {
                if (filter.Group != null)
                {
                    groups.GetList(filter.Group).Add(filter);
                    continue;
                }

                if (filter.IsCvFilter())
                {
                    cv.Add(filter);
                    continue;
                }

                regular.Add(filter);
            }

            regular.ForEach(x => ReorderCheckAndAssignFilter(tree, x, store));
            AssignFilterGroups(tree, store, groups);
            cv.ForEach(x => AssignFilter(tree, x, store));
        }

        private static void AssignFilterGroups(Tree tree, RegistrationStore store, Dictionary<IFilterGroup, List<IFilter>> groups)
        {
            var groupsToAssign = new List<IFilter>();
            var remainingCvFilters = new List<IFilter>();
            foreach (var group in groups)
            {
                var cvFilterNodes = group.Value
                                         .Where(x => x.IsCvFilter())
                                         .Select(x => tree.GetNode(x.Dependencies[0].Definition))
                                         .ToList();
                foreach (var filter in group.Value)
                {
                    if (!filter.Dependencies.Any(x => cvFilterNodes.Any(y => x.GetDependencyNode(tree).IsSourceChildOf(y))))
                    {
                        AddFilterToGroup(group.Key, filter, tree, store);
                        continue;
                    }

                    if (filter.IsCvFilter())
                    {
                        remainingCvFilters.Add(filter);
                    }
                    else
                    {
                        ReorderCheckAndAssignFilter(tree, filter, store);
                    }
                }

                if (group.Key.Dependencies.Any())
                {
                    groupsToAssign.Add(group.Key);
                }
            }

            groupsToAssign.ForEach(x => NodeReorderingService.ReorderNodes(tree, x));
            groupsToAssign.ForEach(x => CheckAndAssignFilter(tree, x, store));
            remainingCvFilters.ForEach(x => AssignFilter(tree, x, store));
        }

        private static void AddFilterToGroup(IFilterGroup group, IFilter filter, Tree tree, RegistrationStore store)
        {
            if (filter.IsCvFilter())
            {
                var node = tree.GetNode(filter.Dependencies[0].Definition);
                if (store.CollectionInversions.Contains(node.Definition))
                {
                    filter = new Inverter(filter);
                }
            }

            group.AddFilter(filter);
        }
    }
}
