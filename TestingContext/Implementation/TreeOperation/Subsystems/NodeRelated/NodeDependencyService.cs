namespace TestingContextCore.Implementation.TreeOperation.Subsystems.NodeRelated
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;

    internal static class NodeDependencyService
    {
        public static List<INode> GetDependencies(this TreeContext context, INode node)
        {
            List<INode> dependencies = node.Provider.Dependencies
                                           .Select(x => context.Tree.GetNode(x.Token))
                                           .ToList();
            context.GetParentGroup(node.Provider)?.AddGroupDependency(context, dependencies);
            return dependencies.Distinct().ToList();
        }

        private static void AddGroupDependency(this IFilterGroup parentGroup,
            TreeContext context,
            List<INode> dependencies)
        {
            dependencies.Add(context.Tree.GetNode(parentGroup.NodeToken));
        }

        public static List<INode> GetDependencies(this TreeContext context, IDepend depend)
        {
            List<INode> dependencies = depend.Dependencies.DistinctDependencies()
                                             .Select(context.GetDependencyNode)
                                             .ToList();
            context.GetParentGroup(depend)?.AddGroupDependency(context, dependencies);
            return dependencies;
        }

        public static INode GetAssignmentNode(this TreeContext context, IDepend depend)
        {
            return context.GetDependencies(depend)
                       .OrderByDescending(x => x.GetParentalChain().Count)
                       .First();
        }
    }
}