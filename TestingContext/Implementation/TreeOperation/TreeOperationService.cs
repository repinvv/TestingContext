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

    internal static class TreeOperationService
    {
        public static Tree CreateTree(TokenStore store)
        {
            ProviderLoopDetectionService.DetectRegistrationsLoop(store);

            var tree = new Tree();
            tree.Root = new RootNode(tree, store.RootToken);
            tree.Nodes.Add(store.RootToken, tree.Root);

            SetupTreeFilters(store, tree);
            var nodeDependencies = GetNodesWithDependencies(store, tree);
            
            //CalculateNodeWeights(tree);
            BuildNodesTree(tree, nodeDependencies);
            ProcessTreeFilters(store, tree);
            AssignFilters(store, tree);
            tree.RootContext = new ResolutionContext<Root>(Root.Instance, tree.Root, null, store);
            return tree;
        }

        private static void CalculateNodeWeights(Tree tree)
        {
            var depends = new List<IDepend>();
            depends.AddRange(tree.Nodes.Select(x => x.Value.Provider));
            depends.AddRange(tree.Filters);
            depends.ForEach(x => x.ForDependencies((dep1, dep2) => SetNodeWeights(tree, dep1, dep2)));
        }

        private static void SetNodeWeights(Tree tree, IDependency dep1, IDependency dep2)
        {
            
        }
    }
}
