namespace TestingContextCore.Implementation.Registrations.FilterRegistrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Implementation.Filters.Groups;

    internal class FilterGroupRegistration : IFilterRegistration
    {
        private readonly Func<IFilterGroup> groupConstructor;

        public FilterGroupRegistration(Func<IFilterGroup> groupConstructor)
        {
            this.groupConstructor = groupConstructor;
        }

        public List<IFilterRegistration> FilterRegistrations { get; } = new List<IFilterRegistration>();

        public IFilter GetFilter()
        {
            var result = groupConstructor();
            result.Filters.AddRange(FilterRegistrations.Select(x => x.GetFilter()));
            return result;
        }
    }
}
