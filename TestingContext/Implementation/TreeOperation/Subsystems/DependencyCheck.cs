namespace TestingContextCore.Implementation.TreeOperation.Subsystems
{
    using System.Linq;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.PublicMembers.Exceptions;

    internal static class DependencyCheck
    {
        private const string Unknown = "unknown";

        public static void CheckDependencies(IProvider provider, IToken token)
        {
            foreach (var dependency in provider.Dependencies.Where(x => x.Token == null))
            {
                throw new RegistrationException($"Dependency of type {dependency.SourceType?.Name ?? Unknown } " +
                                                $"for {token} is not registered.", provider.DiagInfo);
            }
        }

        public static void CheckDependencies(IFilter filter)
        {
            var group = filter as IFilterGroup;
            if (group != null)
            {
                foreach (var groupFilter in group.Filters)
                {
                    CheckDependencies(groupFilter);
                }

                return;
            }

            foreach (var dependency in filter.Dependencies.Where(x => x.Token == null))
            {
                throw new RegistrationException($"Dependency of type {dependency.SourceType?.Name ?? Unknown } " +
                                                $"for a filter is not registered.", filter.DiagInfo);
            }
        }
    }
}
