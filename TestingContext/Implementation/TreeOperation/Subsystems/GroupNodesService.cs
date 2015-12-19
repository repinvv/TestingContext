namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Registrations;
    using static GroupFiltersService;

    internal static class GroupNodesService
    {
        public static void CreateNodesForFilterGroups(IEnumerable<IFilter> filters,
                    Dictionary<IToken, List<INode>> nodeDependencies,
                    TokenStore store,
                    Tree tree)
        {
            foreach (var group in filters.OfType<IFilterGroup>())
            {
                CreateNodeForFilterGroup(group, nodeDependencies, store, tree);
                CreateNodesForFilterGroups(group.Filters, nodeDependencies, store, tree);
            }
        }

        private static void CreateNodeForFilterGroup(IFilterGroup filterGroup,
            Dictionary<IToken, List<INode>> nodeDependencies,
            TokenStore store,
            Tree tree)
        {
            if (filterGroup.GroupToken == filterGroup.Group?.GroupToken)
            {
                return;
            }

            var groupDependencies = new HashSet<IDependency>(filterGroup.GroupDependencies);
            var inGroupTokens = new HashSet<IToken>(GetInGroupTokens(filterGroup, store));
            if (!inGroupTokens.Any())
            {
                return;
            }

            foreach (var dependency in inGroupTokens
                .SelectMany(x => store.Providers[x].Dependencies)
                .Concat(filterGroup.Dependencies)
                .Where(dependency => !inGroupTokens.Contains(dependency.Token)))
            {
                groupDependencies.Add(dependency);
            }
            var provider = new GroupProvider(groupDependencies, filterGroup.Group, store, filterGroup.DiagInfo);
            var node = Node.CreateNode(filterGroup.GroupToken, provider, store, tree);
            node.IsNegative = true;
            tree.Nodes.Add(node.Token, node);
            tree.NodesToCreateExistsFilter.Add(new Tuple<INode, IDiagInfo>(node, filterGroup.DiagInfo));
            foreach (var groupDependency in groupDependencies.Select( x=>x.Token).Distinct())
            {
                nodeDependencies.GetList(groupDependency).Add(node);
            }

            //all in group nodes to depend on created node
            var ingroupNodes = inGroupTokens.Select(inGroupToken => tree.Nodes[inGroupToken]);
            nodeDependencies.GetList(filterGroup.GroupToken).AddRange(ingroupNodes);
        }
    }
}
