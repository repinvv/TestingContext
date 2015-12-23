namespace TestingContextCore.Implementation.TreeOperation
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.TreeOperation.LoopDetection;
    using TestingContextCore.Implementation.TreeOperation.Subsystems;
    using static Subsystems.TreeBuilder;
    using static Subsystems.FilterAssignmentService;
    using static TestingContextCore.Implementation.TreeOperation.Subsystems.FilterProcessingService;
    using static TestingContextCore.Implementation.TreeOperation.Subsystems.NodesCreationService;

    internal static class TreeOperationService
    {
        public static Tree CreateTree(TokenStore store)
        {
            ProviderLoopDetectionService.DetectRegistrationsLoop(store);

            var tree = new Tree { Store = store };
            tree.Root = new RootNode(tree, store.RootToken);
            tree.Nodes.Add(store.RootToken, tree.Root);

            SetupTreeFilters(tree);
            CreateNodes(tree).ForEach(node => tree.Nodes.Add(node.Token, node));
            ProcessTreeFilters(tree);
            var nodeDependencies = GroupNodes(tree);
            NodeWeigthsService.CalculateNodeWeights(tree, nodeDependencies);
            BuildNodesTree(tree, nodeDependencies);
            AssignFilters(tree);
            tree.RootContext = new ResolutionContext<Root>(Root.Instance, tree.Root, null, store);
            return tree;
        }

        private static Dictionary<IToken, List<INode>> GroupNodes(Tree tree)
        {
            var nodes = tree.Nodes.Values;
            var dict = new Dictionary<IToken, List<INode>>();
            foreach (var node in nodes.Where(x => x != tree.Root))
            {
                tree.GetDependencies(node).ForEach(dependency => dict.GetList(dependency.Token).Add(node));
            }

            return dict;
        }
    }
}
