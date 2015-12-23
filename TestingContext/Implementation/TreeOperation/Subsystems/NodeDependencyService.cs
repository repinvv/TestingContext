namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Nodes;

    internal static class NodeDependencyService
    {
        public static List<INode> GetDependencies(this Tree tree, INode node)
        {
            var dependencies = node.Provider.Dependencies.Select(x => tree.GetNode(x.Token)).ToList();
            AddGroupDependency(tree, node.Provider, dependencies);
            return dependencies.Distinct().ToList();
        }

        public static List<INode> GetDependencies(this Tree tree, IDepend depend)
        {
            var dependencies = depend.Dependencies
                                     .GroupBy(x => new { x.Type, x.Token })
                                     .Select(x => tree.GetDependencyNode(x.First()))
                                     .ToList();
            AddGroupDependency(tree, depend, dependencies);
            return dependencies;
        }

        private static void AddGroupDependency(this Tree tree, IDepend depend, List<INode> dependencies)
        {
            var group = tree.GetParentGroup(depend);
            if (group != null)
            {
                dependencies.Add(tree.GetNode(group.NodeToken));
            }
        }

        public static INode GetAssignmentNode(this Tree tree, IDepend depend)
        {
            return tree.GetDependencies(depend)
                       .OrderByDescending(x => x.GetParentalChain().Count)
                       .First();
        }
    }
}
