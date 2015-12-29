namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;

    internal static class NodeDependencyService
    {
        //public static List<INode> GetDependencies(this TreeContext context, INode node)
        //{
        //    List<INode> dependencies = node.Provider.Dependencies
        //                                   .Select(x => context.Tree.GetNode(x.Token))
        //                                   .ToList();
        //    context.AddGroupDependency(node.Provider, dependencies);
        //    return dependencies.Distinct().ToList();
        //}

        //public static List<INode> GetDependencies(this TreeContext context, IDepend depend)
        //{
        //    List<INode> dependencies = depend.Dependencies.GroupBy(x => new { x.Type, x.Token })
        //                                     .Select(x => context.Tree.GetDependencyNode(x.First()))
        //                                     .ToList();
        //    context.AddGroupDependency(depend, dependencies);
        //    return dependencies;
        //}

        //private static void AddGroupDependency(this TreeContext context, IDepend depend, List<INode> dependencies)
        //{
        //    IFilterGroup parentGroup = context.GetParentGroup(depend);
        //    if (parentGroup == null)
        //    {
        //        return;
        //    }

        //    dependencies.Add(context.Tree.GetNode(parentGroup.NodeToken));
        //}

        //public static INode GetAssignmentNode(this TreeContext context, IDepend depend)
        //{
        //    return context.GetDependencies(depend)
        //               .OrderByDescending(x => x.GetParentalChain().Count)
        //               .First();
        //}
    }
}