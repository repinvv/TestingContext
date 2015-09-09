namespace TestingContextCore.Implementation.Sources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Implementation.Resolution;
    using TestingContextCore.Interfaces;

    internal class IndependentSource<T> : Source<TestingContext, T>
        where T : class
    {
        private readonly TestingContext context;
        private object resolveCache;

        public IndependentSource(ContextStore store, TestingContext context, string key, Func<TestingContext, IEnumerable<T>> sourceFunc)
            : base(store, key, sourceFunc, ResolutionType.Independent)
        {
            this.context = context;
        }

        public override IEnumerable<IResolutionContext<T1>> RootResolve<T1>(string key)
        {
            var resolved = resolveCache as IEnumerable<IResolutionContext<T>>;
            if (resolved == null)
            {
               resolveCache = resolved = Resolve<T>(context);
            }

            if (EntityDefinition.Is(typeof(T1), key))
            {
                return resolved as IEnumerable<IResolutionContext<T1>>;
            }

            var first = resolved.FirstOrDefault();

            // root resolve
            return null;
        }

        public override bool IsChildOf(ISource source)
        {
            return false;
        }
    }
}
