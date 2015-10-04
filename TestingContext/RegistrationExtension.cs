namespace TestingContextCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Interfaces;

    public static class RegistrationExtension
    {
        public static void Each<T>(this IFor<IEnumerable<IResolutionContext<T>>> filterRegister, string key = null)
        {
            filterRegister.ThisFilter(x => x.All(y => y.MeetsConditions), key);
        }

        public static void Exists<T>(this IFor<IEnumerable<IResolutionContext<T>>> filterRegister, string key = null)
        {
            filterRegister.ThisFilter(x => x.Any(y => y.MeetsConditions), key);
        }

        public static void DoesNotExist<T>(this IFor<IEnumerable<IResolutionContext<T>>> filterRegister, string key = null)
        {
            filterRegister.ThisFilter(x => !x.Any(y => y.MeetsConditions), key);
        }
    }
}
