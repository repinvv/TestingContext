namespace TestingContextCore.Implementation.Registrations.LoopDetection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TestingContext.Interface;
    using TestingContext.LimitedInterface;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.PublicMembers.Exceptions;
    using TestingContextCore.UsefulExtensions;

    internal static class LoopDetectionService
    {
        private static readonly IToken[] empty = new IToken[0];

        public static void DetectRegistrationLoop(TokenStore store, IProvider provider, IToken token)
        {
            return;
            
            var reliances = GetReliances(provider, token).ToList();
            DetectReliancesLoop(reliances, GetAllReliances(store), provider.CollectionValidityFilter.DiagInfo);
        }

        public static void DetectRegistrationLoop(TokenStore store, IFilter filter)
        {
            return;

            var reliances = GetReliances(filter).ToList();
            DetectReliancesLoop(reliances, GetAllReliances(store), filter.DiagInfo);
        }

        private static List<Reliance> GetAllReliances(TokenStore store)
        {
            var providerReliances = store.Providers.SelectMany(x => GetReliances(x.Value, x.Key));
            var filterReliances = store.Filters.SelectMany(GetReliances);
            return providerReliances.Concat(filterReliances).ToList();
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

        private static void DetectReliancesLoop(List<Reliance> newReliances, List<Reliance> reliances, IDiagInfo diag)
        {
            var allReliances = reliances.Concat(newReliances)
                                        .GroupBy(x => x.Token)
                                        .ToDictionary(x => x.Key, x => x.Select(y => y.ReliesOn).ToArray());
            foreach (var reliance in newReliances)
            {
                var list = Detect(reliance.Token, reliance.Token, allReliances);
                if (list != null)
                {
                    list.Reverse();
                    var line = $"Registration loop is detected for {reliance.Token}{Environment.NewLine}"
                               + GetLoopDetails(reliance.Token, list) +
                               $"which means that prior to determine validity of {reliance.Token},{Environment.NewLine}" +
                               $"all valid items of {reliance.Token} should be known. And this is impossible.";
                    throw new RegistrationsLoopException(reliance.Token, list, line, diag);
                }
            }

            reliances.AddRange(newReliances);
        }

        private static string GetLoopDetails(IToken startingToken, IEnumerable<IToken> list)
        {
            var sb = new StringBuilder();
            foreach (var token in list)
            {
                sb.Append($"validity of {startingToken} depends on all valid items of {token}{Environment.NewLine}");
                startingToken = token;
            }

            return sb.ToString();
        }

        private static List<IToken> Detect(IToken startingToken, IToken currentToken, Dictionary<IToken, IToken[]> allReliances)
        {
            foreach (var reliedOn in allReliances.SafeGet(currentToken, empty))
            {
                if (reliedOn == startingToken)
                {
                    return new List<IToken> { reliedOn };
                }

                var loop = Detect(startingToken, reliedOn, allReliances);
                if (loop != null)
                {
                    loop.Add(reliedOn);
                    return loop;
                }
            }

            return null;
        }
    }
}
