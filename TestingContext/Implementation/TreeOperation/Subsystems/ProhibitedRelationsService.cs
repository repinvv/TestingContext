namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;

    internal static class ProhibitedRelationsService
    {
        public static void FindProhibitedRelations(Tree tree, IFilter filter)
        {
            foreach (IDependency parent in filter.Dependencies)
            {
                if (!parent.IsCollectionDependency)
                {
                    continue;
                }

                foreach (IDependency child in filter.Dependencies.Where(child => parent.Definition != child.Definition))
                {
                    tree.ProhibitedRelations.Add(new ProhibitedRelation(tree.Nodes[parent.Definition], tree.Nodes[child.Definition], filter));
                }
            }
        }
    }
}
