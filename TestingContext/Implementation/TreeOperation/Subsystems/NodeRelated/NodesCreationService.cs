namespace TestingContextCore.Implementation.TreeOperation.Subsystems.NodeRelated
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Providers;

    internal static class NodesCreationService
    {
        public static void CreateNodes(this TreeContext context)
        {
            context.Tree.Nodes = context.CreateProviderNodes().ToDictionary(x => x.Token);
            context.Tree.Nodes.Add(context.Store.RootToken, context.Tree.Root);
            context.CreateGroupNodes();
        }

        private static List<INode> CreateProviderNodes(this TreeContext context)
        {
            return context.Store.Providers
                .Select(x => CreateNode(x.Key, x.Value, context)).ToList();
        }

        private static INode CreateNode(IToken token, IProvider provider, TreeContext context)
        {
            var isNegative = provider.IsNegative || context.Store.CollectionInversions.ContainsKey(token);
            return Node.CreateNode(token, provider, isNegative, context);
        }

        private static void CreateGroupNodes(this TreeContext context)
        {
            foreach (var filterGroup in context.Filters
                .OfType<IFilterGroup>()
                .Where(grp => context.GetInGroupTokens(grp).Any())
                .ToList())
            {
                var node = context.CreateNodeForFilterGroup(filterGroup);
                context.Tree.Nodes.Add(node.Token, node);
                context.Filters.Add(CreateExistsFilter(node, filterGroup));
            }
        }

        private static IFilter CreateExistsFilter(INode node, IFilterGroup filterGroup)
        {
            var dependency = new CollectionDependency(node.Token);
            return new ExistsFilter(dependency, filterGroup.FilterInfo);
        }

        private static INode CreateNodeForFilterGroup(this TreeContext context, IFilterGroup filterGroup)
        {
            var groupDependencies = GroupFiltersService.GetGroupDependencies(context, filterGroup);
            var provider = new GroupProvider(groupDependencies, filterGroup, context.Store, filterGroup.DiagInfo);
            return Node.CreateNode(filterGroup.NodeToken, provider, true, context);
        }
    }
}
