namespace TestingContextCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using TestingContextCore.Interfaces;

    public static class RegistrationExtension
    {
        public static void ConditionedFilter<T>(this IFor<IEnumerable<IResolutionContext<T>>> filterRegister, Func<IEnumerable<T>, bool> filter)
        {
            filterRegister.ThisFilter(x => filter(x.Where(y => y.MeetsConditions).Select(y => y.Value)));
        }

        public static void Each<T>(this IFor<IEnumerable<IResolutionContext<T>>> filterRegister)
        {
            filterRegister.ThisFilter(x => x.All(y => y.MeetsConditions));
        }

        public static void Exists<T>(this IFor<IEnumerable<IResolutionContext<T>>> filterRegister)
        {
            filterRegister.ThisFilter(x => x.Any(y => y.MeetsConditions));
        }

        public static void DoesNotExist<T>(this IFor<IEnumerable<IResolutionContext<T>>> filterRegister)
        {
            filterRegister.ThisFilter(x => !x.Any(y => y.MeetsConditions));
        }
    }
}
