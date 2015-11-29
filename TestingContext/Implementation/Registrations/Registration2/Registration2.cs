namespace TestingContextCore.Implementation.Registrations.Registration2
{
    using TestingContextCore.Interfaces;

    internal partial class Registration2<T1, T2> : IFor<T1, T2>
    {
        private readonly InnerRegistration2<T1, T2> inner;
        private readonly TokenStore store;

        public Registration2(TokenStore store, InnerRegistration2<T1,T2> inner)
        {
            this.inner = inner;
            this.store = store;
        }
    }
}
