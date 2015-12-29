namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Resolution;
    using static FilterAssignmentService;

    internal static class NonEqualFilteringService
    {
        //public static void AssignNonEqualFilter(TreeContext context, INode node1, INode node2, IFilterGroup group, IDiagInfo diagInfo)
        //{
        //    if (node1.Token.Type != node2.Token.Type)
        //    {
        //        return;
        //    }

        //    var tuple = new Tuple<IToken, IToken>(node1.Token, node2.Token);
        //    var reverseTuple = new Tuple<IToken, IToken>(node2.Token, node1.Token);
        //    if (context.NonEqualFilters.Contains(tuple) || context.NonEqualFilters.Contains(reverseTuple))
        //    {
        //        return;
        //    }

        //    context.NonEqualFilters.Add(tuple);
        //    var dep1 = new SingleDependency(node1.Token);
        //    var dep2 = new SingleDependency(node2.Token);
            
        //    var info = new FilterInfo(context.Store.NextId, diagInfo, group?.FilterInfo.FilterToken);
        //    var filter = new Filter2<IResolutionContext, IResolutionContext>(dep1, dep2, (x, y) => !x.Equals(y), info);
        //    AssignFilter(context, filter);
        //}
    }
}
