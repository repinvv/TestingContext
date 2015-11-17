namespace TestingContextCore.Implementation.Registrations
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Providers;
    using TestingContextCore.Implementation.TreeOperation;

    internal class RegistrationStore
    {
        public RegistrationStore(Definition rootDefinition)
        {
            RootDefinition = rootDefinition;
        }
        
        public Definition RootDefinition { get; }

        public IDictionary<Definition, IProvider> Providers { get; } = new Dictionary<Definition, IProvider>();

        public List<IFilter> Filters { get; } = new List<IFilter>();

        public HashSet<string> FilterInversions { get; } = new HashSet<string>();

        public HashSet<Definition> CollectionInversions { get; } = new HashSet<Definition>();

        public HashSet<Definition> ItemInversions { get; } = new HashSet<Definition>();

        public Tree Tree { get; set; }
    }
}
