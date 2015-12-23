namespace TestingContextCore.Implementation.TreeOperation.LoopDetection
{
    using System.Collections.Generic;
    using System.Linq;
    using Filters;
    using TestingContextCore.Implementation.Nodes;

    internal static class FiltersLoopDetectionService
    {
        public static void DetectFilterDependenciesLoop(Tree tree)
        {
            var vertices = tree.Nodes.Values.SelectMany(GetVertices);
        }

        private static IEnumerable<FilterVertex> GetVertices(INode node)
        {
            return node.FilterInfo.Group.Filters.SelectMany(filter => GetVertices(node, filter));
        }

        private static IEnumerable<FilterVertex> GetVertices(INode node, IFilter filter)
        {
            var vertices = new List<FilterVertex>();
            foreach (var VARIABLE in COLLECTION)
            {
                
            }
        }
    }
}
