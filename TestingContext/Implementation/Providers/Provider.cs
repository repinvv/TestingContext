namespace TestingContextCore.Implementation.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.Dependencies;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Implementation.ResolutionContext;

    internal class Provider<TSource, T> : IProvider
    {
        private readonly IDependency<TSource> dependency;
        private readonly Func<TSource, IEnumerable<T>> sourceFunc;
        private readonly ProviderDetails details;

        public Provider(IDependency<TSource> dependency,
            Func<TSource, IEnumerable<T>> sourceFunc,
            ProviderDetails details)
        {
            this.dependency = dependency;
            this.sourceFunc = sourceFunc;
            this.details = details;
        }

        public Definition Definition => details.Definition;

        public IResolution Resolve(IResolutionContext parentContext)
        {
            var source = sourceFunc(dependency.GetValue(parentContext))
                .Select(x => new ResolutionContext<T>(x, Definition, parentContext, details.Filters, details.ChildProviders));
            return new Resolution<T>(Definition, source);
        }
    }
}
