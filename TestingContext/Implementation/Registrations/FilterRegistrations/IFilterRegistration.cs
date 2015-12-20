namespace TestingContextCore.Implementation.Registrations.FilterRegistrations
{
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;

    internal interface IFilterRegistration
    {
        IFilter GetFilter(IFilterGroup group);
    }
}
