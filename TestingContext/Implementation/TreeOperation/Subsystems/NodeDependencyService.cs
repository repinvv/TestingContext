namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;

    internal static class NodeDependencyService
    {
        public static List<INode> GetDependencies(this Tree tree, INode node)
        {
            List<INode> dependencies = node.Provider.Dependencies
                                           .Select(x => tree.GetNode(x.Token))
                                           .ToList();
            tree.AddGroupDependency(node.Provider, dependencies);
            return dependencies.Distinct().ToList();
        }

        public static List<INode> GetDependencies(this Tree tree, IDepend depend)
        {
            List<INode> dependencies = depend.Dependencies.GroupBy(x => new { x.Type, x.Token })
                                             .Select(x => tree.GetDependencyNode(x.First()))
                                             .ToList();
            tree.AddGroupDependency(depend, dependencies);
            return dependencies;
        }

        private static void AddGroupDependency(this Tree tree, IDepend depend, List<INode> dependencies)
        {
            IFilterGroup parentGroup = tree.GetParentGroup(depend);
            if (parentGroup == null)
            {
                return;
            }

            dependencies.Add(tree.GetNode(parentGroup.NodeToken));
        }

        public static INode GetAssignmentNode(this Tree tree, IDepend depend)
        {
            return tree.GetDependencies(depend)
                       .OrderByDescending(x => x.GetParentalChain().Count)
                       .First();
        }
    }
}