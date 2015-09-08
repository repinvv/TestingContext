namespace TestingContextCore.Implementation.Sources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Implementation.ContextStorage;
    using TestingContextCore.Implementation.Registrations;
    using TestingContextCore.Interfaces;

    internal class IndependentSource<T> : Source<TestingContext, T>
    {
        private readonly TestingContext context;
        private object resolveCache;

        public IndependentSource(ContextStore store, TestingContext context, string key, Func<TestingContext, IEnumerable<T>> sourceFunc)
            : base(store, key, sourceFunc)
        {
            this.context = context;
        }

        public override IEnumerable<IResolutionContext<T1>> Resolve<T1>(string key)
        {
            var resolve = resolveCache as IEnumerable<IResolutionContext<T1>>;
            if (resolve == null)
            {
               resolveCache = resolve = Resolve<T1>();
            }

            if (EntityDefinition.Is(typeof(T), key))
            {
                return resolve;
            }

            var first = resolve.FirstOrDefault();

            // root resolve
            return null;
        }

        private IEnumerable<IResolutionContext<T1>> Resolve<T1>()
        {
            throw new NotImplementedException();
        }

        public override bool IsChildOf(ISource source)
        {
            return false;
        }
    }
}
