namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;

    internal static class CvFilterExtension
    {
        public static bool IsCvFilter(this IFilter filter) => filter.Dependencies.FirstOrDefault()?.Type == DependencyType.CollectionValidity;
    }
}
