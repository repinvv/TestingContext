namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registration;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;
    using static FilterAssignmentService;

    internal static class NonEqualFilteringService
    {
        public static void AssignNonEqualFilter(TokenStore store, INode node1, INode node2)
        {
            if (node1 == node2 || node1.Token.Type != node2.Token.Type)
            {
                return;
            }

            var tuple = new Tuple<IToken, IToken>(node1.Token, node2.Token);
            var reverseTuple = new Tuple<IToken, IToken>(node2.Token, node1.Token);
            if (store.Tree.NonEqualFilters.Contains(tuple) || store.Tree.NonEqualFilters.Contains(reverseTuple))
            {
                return;
            }

            store.Tree.NonEqualFilters.Add(tuple);
            var dep1 = new NonGenericDependency(node1.Token);
            var dep2 = new NonGenericDependency(node2.Token);
            var dummyDiag = DiagInfo.Create(string.Empty, 0, $"Non-equal filter for {node1.Token} and {node2.Token}");
            var filter = new Filter2<IResolutionContext, IResolutionContext>(dep1, dep2, (x, y) => !x.Equals(y), dummyDiag, null);
            AssignFilter(store, filter);
        }
    }
}
