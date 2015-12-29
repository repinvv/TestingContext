namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Linq;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;

    internal static class FilterAssignmentService
    {
        //public static void AssignFilter(TreeContext context, IFilter filter)
        //{
        //    if (!filter.Dependencies.Any())
        //    {
        //        return;
        //    }
            
        //    var node = context.Tree.GetNode(filter as IFilterGroup) ?? context.GetAssignmentNode(filter);
        //    AssignFilterToNode(filter, node);
        //}

        //public static void AssignFilterToNode(IFilter filter, INode node)
        //{
        //    node.FilterInfo.Group.Filters.Add(filter);
        //}

        //public static void AssignFilters(Tree tree)
        //{
        //    // tree.Filters.ForEach(x => AssignFilter(tree, x));
        //}
    }
}
