namespace TestingContextCore.Implementation.TreeOperation
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.TreeOperation.LoopDetection;
    using TestingContextCore.Implementation.TreeOperation.Subsystems;
    using static Subsystems.TreeBuilder;
    using static Subsystems.FilterAssignmentService;
    using static TestingContextCore.Implementation.TreeOperation.Subsystems.FilterProcessingService;
    using static TestingContextCore.Implementation.TreeOperation.Subsystems.NodesCreationService;
    using System;
    using TestingContext.LimitedInterface.Tokens;

    internal static class TreeOperationService
    {
        public static Tree CreateTree(TokenStore store)
        {
            ProviderLoopDetectionService.DetectRegistrationsLoop(store);

            var tree = new Tree { Store = store };
            tree.Root = new RootNode(tree, store.RootToken);
            tree.Nodes.Add(store.RootToken, tree.Root);

            SetupTreeFilters(tree);
            var nodeDependencies = GetNodesWithDependencies(tree);
            ProcessTreeFilters(tree);
            NodeWeigthsService.CalculateNodeWeights(tree, nodeDependencies);
            BuildNodesTree(tree, nodeDependencies);
            AssignFilters(tree);
            tree.RootContext = new ResolutionContext<Root>(Root.Instance, tree.Root, null, store);
            return tree;
        }
    }
}
