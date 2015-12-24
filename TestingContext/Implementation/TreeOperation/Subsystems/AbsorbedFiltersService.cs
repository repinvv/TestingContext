namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Nodes;

    internal static class AbsorbedFiltersService
  {
    public static void ExtractAbsorbedFilters(this IFilterGroup group, List<IFilter> freeFilters, Tree tree)
    {
      var cvFilters = group.Filters.Where(tree.IsCvFilter).ToList();
      var absorbedFilters = group.Filters.Where(x => FilterIsAbsorbed(tree, cvFilters, x)).ToList();
      freeFilters.AddRange(absorbedFilters);
      group.Filters.ForGroups(grp => grp.ExtractAbsorbedFilters(freeFilters, tree));
      group.Filters = group.Filters.Except(absorbedFilters).ToList();
    }

    private static bool FilterIsAbsorbed(Tree tree, List<IFilter> cvFilters, IFilter filter)
    {
      return cvFilters.Where(x => x != filter).Any(x => FilterIsAbsorbedBy(filter, tree.GetCvFilterNode(x), tree));
    }

    private static bool FilterIsAbsorbedBy(IFilter filter, INode cvNode, Tree tree)
    {
      return filter.Dependencies.Any(x => DependencyIsAbsorbed(x, cvNode, tree));
    }

    private static bool DependencyIsAbsorbed(IDependency dependency, INode cvNode, Tree tree)
    {
      if (dependency.Type == DependencyType.Single && dependency.Token == cvNode.Token)
        return true;
      return tree.IsParent(dependency.Token, cvNode.Token);
    }
  }
}