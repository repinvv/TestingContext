namespace TestingContextCore.Implementation.Registrations.FilterRegistrations
{
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;

    internal interface IFilterRegistration
    {
        int Id { set; }
        
        IFilter GetFilter(IFilterGroup group);
    }
}
