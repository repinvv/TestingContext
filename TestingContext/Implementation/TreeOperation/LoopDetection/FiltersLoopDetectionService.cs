namespace TestingContextCore.Implementation.TreeOperation.LoopDetection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Filters;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.PublicMembers.Exceptions;
    using TestingContextLimitedInterface.Diag;
    using TestingContextLimitedInterface.Tokens;

    internal static class FiltersLoopDetectionService
    {
        public static void DetectFilterDependenciesLoop(Tree tree)
        {
            var vertices = tree.Nodes.Values.SelectMany(GetVertices).ToList();
            var lookup = vertices.ToLookup(x => x.Token);
            foreach (var vertex in vertices)
            {
                vertex.Dependencies.AddRange(lookup[vertex.Dependency]);
            }

            foreach (var loop in new TarjanLoopDetection<FilterVertex>().DetectLoop(vertices).Where(x => x.Count > 1))
            {
                var types = string.Join(",", loop.Select(x => x.Token));
                var tuples = loop.Select(x => new Tuple<IToken, IDiagInfo>(x.Token, x.Filter.DiagInfo)).ToList();
                throw new DetailedRegistrationException("Following types declare a dependency loop: " + types, tuples);
            }
        }

        private static IEnumerable<FilterVertex> GetVertices(INode node)
        {
            return node.FilterInfo.Group.Filters.SelectMany(filter => GetVertices(node.Token, filter));
        }

        private static IEnumerable<FilterVertex> GetVertices(IToken token, IFilter filter)
        {
            var group = filter as IFilterGroup;
            if (group != null)
            {
                return group.Filters.SelectMany(f => GetVertices(token, f));
            }

            return filter.Dependencies
                         .Where(x => x.Type != DependencyType.Single)
                         .Select(dep => new FilterVertex
                                        {
                                            Filter = filter,
                                            Dependency = dep.Token,
                                            Token = token
                                        });
        }
    }
}
