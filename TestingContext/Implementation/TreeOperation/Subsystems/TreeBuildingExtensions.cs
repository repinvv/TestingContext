namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;
    using TestingContextCore.Implementation.Registrations;

    internal static class TreeBuildingExtensions
    {
        public static bool IsCvFilter(this TokenStore store, IFilter filter)
        {
            return store.CvFilters.Contains(filter.FilterInfo.Token);
        }

        // can be used after the tree is built
        public static INode GetDependencyNode(this IDependency dependency, Tree tree)
        {
            var node = tree.GetNode(dependency.Token);
            return dependency.Type == DependencyType.Collection
                ? node.SourceParent
                : node;
        }

        public static void ForGroups(this List<IFilter> filters, Action<IFilterGroup> action)
        {
            foreach (var group in filters.OfType<IFilterGroup>())
            {
                action(group);
                ForGroups(group.Filters, action);
            }
        }

        public static void ForDependencies(this IDepend depend, Action<IDependency, IDependency> action)
        {
            var dependencies = depend.Dependencies.ToArray();
            for (int i = 0; i < dependencies.Length; i++)
            {
                for (int j = i + 1; j < dependencies.Length; j++)
                {
                    action(dependencies[i], dependencies[j]);
                }
            }
        }

        public static IFilterGroup GetParentGroup(this Tree tree, IDepend depend)
        {
            return depend.GroupToken == null 
                ? null :
                tree.FilterGroups[depend.GroupToken];
        }
    }
}
