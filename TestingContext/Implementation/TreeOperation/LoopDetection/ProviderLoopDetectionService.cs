namespace TestingContextCore.Implementation.TreeOperation.LoopDetection
{
    using System;
    using System.Linq;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.PublicMembers.Exceptions;

    internal static class ProviderLoopDetectionService
    {
        public static void DetectRegistrationsLoop(TokenStore store)
        {
            var vertices = store.Providers.Select(x => new ProviderVertex { Provider = x.Value, Token = x.Key })
                                .ToDictionary(x => x.Token);
            var rootVertex = new ProviderVertex { Token = store.RootToken };
            vertices.Add(store.RootToken, rootVertex);
            foreach (var vertex in vertices.Values.Where(x=>x.Token != store.RootToken))
            {
                foreach (var dependency in vertex.Provider.Dependencies)
                {
                    vertices[dependency.Token].Dependencies.Add(vertex);
                }
            }

            foreach(var loop in new TarjanLoopDetection<ProviderVertex>().DetectLoop(vertices.Values.ToArray()).Where(x=>x.Count > 1))
            {
                var types = string.Join(",", loop.Select(x => x.Token));
                var tuples = loop.Select(x => new Tuple<IToken, IDiagInfo>(x.Token, x.Provider.DiagInfo)).ToList();
                throw new DetailedRegistrationException("Following types declare a dependency loop" + types, tuples);
            }
        }

        public static void DetectRegistrationLoop(TokenStore store, IFilter filter)
        { }
    }
}