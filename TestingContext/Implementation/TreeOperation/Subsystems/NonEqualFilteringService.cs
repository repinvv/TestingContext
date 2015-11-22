namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registration;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;
    using static FilterAssignmentService;

    internal static class NonEqualFilteringService
    {
        public static void AssignNonEqualFilters(Tree tree, IHaveDependencies have, TokenStore store)
        {
            var dependencies = have.Dependencies.ToArray();
            for (int i = 0; i < dependencies.Length; i++)
            {
                for (int j = i + 1; j < dependencies.Length; j++)
                {
                    var node1 = dependencies[i].GetDependencyNode(tree);
                    var node2 = dependencies[j].GetDependencyNode(tree);
                    AssignNonEqualFilter(tree, node1, node2);
                }
            }
        }

        private static void AssignNonEqualFilter(Tree tree, INode node1, INode node2)
        {
            if (node1 == node2 || node1.Token.Type != node2.Token.Type)
            {
                return;
            }

            var tuple = new Tuple<IToken, IToken>(node1.Token, node2.Token);
            var reverseTuple = new Tuple<IToken, IToken>(node2.Token, node1.Token);
            if (tree.NonEqualFilters.Contains(tuple) || tree.NonEqualFilters.Contains(reverseTuple))
            {
                return;
            }

            tree.NonEqualFilters.Add(tuple);
            var dep1 = new NonGenericDependency(node1.Token);
            var dep2 = new NonGenericDependency(node2.Token);
            var dummyDiag = new DiagInfo(string.Empty, 0, "Non-equal filter");
            var filter = new Filter2<IResolutionContext, IResolutionContext>(dep1, dep2, (x, y) => !x.Equals(y), dummyDiag);
            AssignFilter(tree, filter);
        }
    }
}
