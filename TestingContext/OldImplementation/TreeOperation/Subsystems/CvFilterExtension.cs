namespace TestingContextCore.OldImplementation.TreeOperation.Subsystems
{
    using TestingContextCore.OldImplementation.Dependencies;
    using TestingContextCore.OldImplementation.Filters;

    internal static class CvFilterExtension
    {
        public static bool IsCvFilter(this IFilter filter) => filter.Dependencies.Length == 1 && filter.Dependencies[0].Type == DependencyType.CollectionValidity;
    }
}
