namespace TestingContextCore.Implementation.TreeOperation
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.TreeOperation.LoopDetection;
    using TestingContextCore.Implementation.TreeOperation.Subsystems;
    using static Subsystems.TreeBuilder;
    using static Subsystems.FilterAssignmentService;
    using static Subsystems.FilterProcessingService;
    using static Subsystems.NodesCreationService;

    internal static class TreeOperationService
    {
        public static Tree CreateTree(TokenStore store)
        {
            ProviderLoopDetectionService.DetectRegistrationsLoop(store);

            var tree = new Tree();
            tree.Root = new RootNode(tree, store.RootToken);
            var context = TreeContextService.CreateTreeContext(store, tree);
            context.CreateNodes();
            var nodeDependencies = GroupNodes(context);
            NodeWeigthsService.CalculateNodeWeights(context, nodeDependencies);
            BuildNodesTree(context, nodeDependencies);
            ReorderNodesForFilters(context);
            GetFinalFilters(context);
            AssignFilters(tree);
            int i = 0;
            tree.FilterIndex = context.Filters.ToDictionary(x => x, x => i++);
            tree.RootContext = new ResolutionContext<Root>(Root.Instance, tree.Root, null, store);
            return tree;
        }

        private static Dictionary<IToken, List<INode>> GroupNodes(TreeContext context)
        {
            var nodes = context.Tree.Nodes.Values.Where(x => x != context.Tree.Root);
            var dict = new Dictionary<IToken, List<INode>>();
            foreach (var node in nodes)
            {
                context.GetDependencies(node).ForEach(dependency => dict.GetList(dependency.Token).Add(node));
            }

            return dict;
        }
    }
}
