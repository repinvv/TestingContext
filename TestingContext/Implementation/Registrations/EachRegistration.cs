namespace TestingContextCore.Implementation.Registrations
{
    using TestingContextCore.Implementation.ContextStore;

    internal class EachRegistration<TDepend> : DependentRegistration<TDepend>
    {
        public EachRegistration(string dependKey, ContextStore store) : base(dependKey, store)
        { }
    }
}
