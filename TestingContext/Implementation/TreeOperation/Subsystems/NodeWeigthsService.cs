namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Nodes;

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

        private static void SetNodeWeights(Tree tree, IDependency dependency, IDependency dep2)
        {
            if (tree.IsParent(dependency.Token, dep2.Token) || tree.IsParent(dep2.Token, dependency.Token))
            {
                return;
            }

            SetNodeWeights(tree, dependency);
            SetNodeWeights(tree, dep2);
        }

        private static void SetNodeWeights(Tree tree, IDependency dependency)
        {
            if (tree.WeightedDependencies.Contains(dependency))
            {
                return;
            }

            tree.WeightedDependencies.Add(dependency);
            GetDependencyNodes(tree, dependency)
                .OrderByDescending(x => x.Weight)
                .First()
                .Weight++;
        }

        private static IEnumerable<INode> GetDependencyNodes(Tree tree, IDependency dep1)
        {
            return dep1.Type == DependencyType.Single 
                ? new[] { tree.Nodes[dep1.Token] } 
                : tree.Nodes[dep1.Token].Provider.Dependencies.SelectMany(x => GetDependencyNodes(tree, x));
        }
    }
}
