namespace TestingContextCore.Implementation.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContext.LimitedInterface.Tokens;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.PublicMembers.Exceptions;

    internal class Filter1<T1> : BaseFilter, IFilter
    {
        private readonly IDependency<T1> dependency;
        private readonly Func<T1, bool> filter;

        public Filter1(IDependency<T1> dependency, Func<T1, bool> filter, FilterInfo info) 
            : base(info)
        {
            this.dependency = dependency;
            this.filter = filter;
            Dependencies = new IDependency[] { dependency };
        }

        public IFilter GetFailingFilter(IResolutionContext context)
        {
            T1 argument = dependency.GetValue(context);
            try
            {
                return filter(argument) ? null : this;
            }
            catch (Exception ex)
            {
                throw new RegistrationException($"Exception in registered expression for a filter", DiagInfo, ex);
            }
        }

        public IEnumerable<IDependency> Dependencies { get; }
        public IEnumerable<IToken> ForTokens => Dependencies.Select(x => x.Token);

        public override string ToString()
        {
            return $"filter for {Dependencies.First().Token}, Id: {FilterInfo.Id}";
        }
    }
}
