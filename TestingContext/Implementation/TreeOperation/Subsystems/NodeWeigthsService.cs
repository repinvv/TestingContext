namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Permissions;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContext.LimitedInterface.UsefulExtensions;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;

    internal static class NodeWeigthsService
    {
        public static void CalculateNodeWeights(Tree tree, Dictionary<IToken, List<INode>> nodeDependencies)
        {
            nodeDependencies.ForNodes(tree, node => SetNodeWeights(tree, node));
            tree.Filters.ForEach(x => x.ForDependencies((dep1, dep2) => SetNodeWeights(tree, dep1, dep2)));
        }

        private static void SetNodeWeights(Tree tree, INode node)
        {
            node.Provider.ForDependencies((dep1, dep2) => SetNodeWeights(tree, dep1, dep2));
        }

        private static void SetNodeWeights(Tree tree, IDependency dep1, IDependency dep2)
        {
            if (tree.IsParent(dep1.Token, dep2.Token) || tree.IsParent(dep2.Token, dep1.Token))
            {
                return;
            }

            SetNodeWeights(tree, dep1);
            SetNodeWeights(tree, dep2);
        }

        private static void SetNodeWeights(Tree tree, IDependency dep1)
        {
            GetDependencyNodes(tree, dep1)
                .OrderByDescending(x => x.Weight)
                .First()
                .Weight++;
        }

        private static IEnumerable<INode> GetDependencyNodes(Tree tree, IDependency dep1)
        {
            return dep1.Type == DependencyType.Single 
                ? new[] { tree.Nodes[dep1.Token] } 
                : tree.Store.Providers[dep1.Token].Dependencies.SelectMany(x => GetDependencyNodes(tree, x));
        }
    }
}
