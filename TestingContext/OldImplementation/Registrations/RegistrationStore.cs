namespace TestingContextCore.OldImplementation.Registrations
{
    using System.Collections.Generic;
    using TestingContextCore.OldImplementation.Filters;
    using TestingContextCore.OldImplementation.Providers;
    using TestingContextCore.OldImplementation.TreeOperation;

    internal class RegistrationStore
    {
        public Definition RootDefinition { get; } = Definition.Define<Root>(null, null);

        public IDictionary<Definition, IProvider> Providers { get; } = new Dictionary<Definition, IProvider>();

        public List<IFilter> Filters { get; } = new List<IFilter>();

        public HashSet<string> FilterInversions { get; } = new HashSet<string>();

        public HashSet<Definition> CollectionInversions { get; } = new HashSet<Definition>();

        public HashSet<Definition> ItemInversions { get; } = new HashSet<Definition>();

        public Tree Tree { get; set; }
    }
}
