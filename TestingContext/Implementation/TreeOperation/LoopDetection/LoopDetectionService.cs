namespace TestingContextCore.Implementation.TreeOperation.LoopDetection
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.Registrations;

    internal static class LoopDetectionService
    {
        public static void DetectRegistrationLoop(TokenStore store, IProvider provider, IToken token)
        {
            return;
        }

        public static void DetectRegistrationLoop(TokenStore store, IFilter filter)
        {
            return;
        }

        private static IEnumerable<Reliance> GetReliances(IProvider provider, IToken token)
        {
            var providerReliesOn = provider
                .CollectionDependencies()
                .Where(x => x.Token != null)
                .Select(dependency => new Reliance { Token = token, ReliesOn = dependency.Token });
            var relyOnProvider = provider
                .SingleDependencies()
                .Where(x => x.Token != null)
                .Select(dependency => new Reliance { Token = dependency.Token, ReliesOn = token });
            return providerReliesOn.Concat(relyOnProvider);
        }

        private static IEnumerable<IDependency> CollectionDependencies(this IDepend have)
            => have.Dependencies.Where(dependency => dependency.Type == DependencyType.Collection);

        private static IEnumerable<IDependency> SingleDependencies(this IDepend have)
            => have.Dependencies.Where(dependency => dependency.Type != DependencyType.Collection);

        private static IEnumerable<Reliance> GetReliances(IFilter filter)
        {
            var group = filter as IFilterGroup;
            if (group != null)
            {
                return group.Filters.SelectMany(GetReliances);
            }

            var collectionDependencies = filter.CollectionDependencies().ToList();
            var list = new List<Reliance>();
            foreach (IDependency dependency in filter.SingleDependencies().Where(x => x.Token != null))
            {
                foreach (var collectionDependency in collectionDependencies.Where(x => x.Token != null))
                {
                    list.Add(new Reliance { Token = dependency.Token, ReliesOn = collectionDependency.Token });
                }
            }

            return list;
        }
    }
}
