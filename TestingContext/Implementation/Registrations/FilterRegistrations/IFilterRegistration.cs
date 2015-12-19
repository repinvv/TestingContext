namespace TestingContextCore.Implementation.Registrations.FilterRegistrations
{
    using TestingContextCore.Implementation.Filters;

    internal interface IFilterRegistration
    {
        IFilter GetFilter();
    }
}
