namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Nodes;

    internal static class NodeWeigthsService
    {
        public static void CalculateNodeWeights(TreeContext context, Dictionary<IToken, List<INode>> nodeDependencies)
        {
            nodeDependencies.ForNodes(context, node => SetNodeWeights(context, node));
            context.Filters.ForEach(x => x.ForDependencies((dep1, dep2) => SetNodeWeights(context, dep1, dep2)));
        }

        private static void SetNodeWeights(TreeContext context, INode node)
        {
            node.Provider.ForDependencies((dep1, dep2) => SetNodeWeights(context, dep1, dep2));
        }

        private static void SetNodeWeights(TreeContext context, IDependency dependency, IDependency dep2)
        {
            if (context.IsParent(dependency.Token, dep2.Token) || context.IsParent(dep2.Token, dependency.Token))
            {
                return;
            }

            SetNodeWeights(context, dependency);
            SetNodeWeights(context, dep2);
        }

        private static void SetNodeWeights(TreeContext context, IDependency dependency)
        {
            if (context.WeightedDependencies.Contains(dependency))
            {
                return;
            }

            context.WeightedDependencies.Add(dependency);
            GetDependencyNodes(context.Tree, dependency)
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
