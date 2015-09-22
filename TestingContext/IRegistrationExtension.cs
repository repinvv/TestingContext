namespace TestingContextCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Interfaces;

    public static class RegistrationExtension
    {
        public static void CollectionFilter<T>(this IFor<IEnumerable<IResolutionContext<T>>> filterRegister, Func<IEnumerable<T>, bool> filter)
        {
            filterRegister.Filter(x => filter(x.Where(y => y.MeetsConditions).Select(y => y.Value)));
        }

        public static void Each<T>(this IFor<IEnumerable<IResolutionContext<T>>> filterRegister)
        {
            filterRegister.Filter(x => x.All(y => y.MeetsConditions));
        }

        public static void Exists<T>(this IFor<IEnumerable<IResolutionContext<T>>> filterRegister)
        {
            filterRegister.CollectionFilter(x => x.Any());
        }

        public static void DoesNotExist<T>(this IFor<IEnumerable<IResolutionContext<T>>> filterRegister)
        {
            filterRegister.CollectionFilter(x => !x.Any());
        }
    }
}
