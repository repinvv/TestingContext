namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using TestingContextCore.Implementation.Filters;
    using static TestingContextCore.Implementation.Dependencies.DependencyType;

    internal static class CvFilterExtension
    {
        public static bool IsCvFilter(this IFilter filter) => filter.Dependencies.Length == 1 && filter.Dependencies[0].Type == CollectionValidity;
    }
}
