namespace TestingContextCore.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.Interfaces.Tokens;

    public static class InterfaceExtensions
    {
        public static IRegister Not(this IRegister register)
        {
            IRegister output = null;
            register.Not(x => output = x);
            return output;
        }

        #region Inversions
        public static void InvertFilter(this ITestingContext context, string name) => context.InvertFilter(context.GetToken(name));
        public static void InvertCollectionValidity<T>(this ITestingContext context, string name) 
            => context.InvertCollectionValidity(context.GetToken<T>(name));
        public static void InvertItemValidity<T>(this ITestingContext context, string name)
            => context.InvertCollectionValidity(context.GetToken<T>(name));
        #endregion

        #region Value Extension
        public static IEnumerable<IResolutionContext<T>> All<T>(this ITestingContext context, string name) 
            => context.All(context.GetToken<T>(name));
        public static T Value<T>(this ITestingContext context, IToken<T> token) 
            => context.All(token).Select(x => x.Value).FirstOrDefault();
        public static T Value<T>(this ITestingContext context, string name) 
            => context.All(context.GetToken<T>(name)).Select(x => x.Value).FirstOrDefault();
        #endregion
    }
}
