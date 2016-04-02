namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextLimitedInterface.Diag;
    using TestingContextLimitedInterface.Tokens;

    internal static class NonEqualFilteringService
    {
        public static void CreateNonEqualFilter(this TreeContext context, 
            INode node1,
            INode node2, 
            IFilterGroup parentGroup,
            IDiagInfo diagInfo)
        {
            if (node1.Token.Type != node2.Token.Type)
            {
                return;
            }

            var tuple = new Tuple<IToken, IToken>(node1.Token, node2.Token);
            var reverseTuple = new Tuple<IToken, IToken>(node2.Token, node1.Token);
            if (context.NonEqualFilters.Contains(tuple) || context.NonEqualFilters.Contains(reverseTuple))
            {
                return;
            }

            context.NonEqualFilters.Add(tuple);
            var dep1 = new SingleDependency(node1.Token);
            var dep2 = new SingleDependency(node2.Token);

            var newInfo = new FilterInfo(context.Store.NextId, diagInfo, parentGroup?.FilterInfo.FilterToken);
            var filter = new Filter2<IResolutionContext, IResolutionContext>(dep1, dep2, (x, y) => !x.Equals(y), newInfo);
            context.Filters.Add(filter);
        }
    }
}
