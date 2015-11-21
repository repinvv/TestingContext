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

        #region IFor1
        public static IFor<T> For<T>(this IRegister register, IHaveToken<T> haveToken) => register.For(x => haveToken.Token);

        public static IFor<T> For<T>(this IRegister register, string name) => register.For(x => x.GetToken<T>(name));

        public static IFor<IEnumerable<T>> ForCollection<T>(this IRegister register, IHaveToken<T> haveToken) 
            => register.ForCollection(x => haveToken.Token);

        public static IFor<IEnumerable<T>> ForCollection<T>(this IRegister register, string name) 
            => register.ForCollection(x => x.GetToken<T>(name));
        #endregion

        #region IFor2
        public static IFor<T1, T2> For<T1, T2>(this IFor<T1> ifor, IHaveToken<T2> haveToken) => ifor.For(x => haveToken.Token);

        public static IFor<T1, IEnumerable<T2>> ForCollection<T1, T2>(this IFor<T1> ifor, IHaveToken<T2> haveToken) 
            => ifor.ForCollection(x => haveToken.Token);
        #endregion

        #region Inversions
        public static void InvertFilter(this ITestingContext context, string name) => context.InvertFilter(context.GetToken(name));

        public static void InvertCollectionValidity<T>(this ITestingContext context, string name) 
            => context.InvertCollectionValidity(context.GetToken<T>(name));

        public static void InvertItemValidity<T>(this ITestingContext context, string name)
            => context.InvertCollectionValidity(context.GetToken<T>(name));
        #endregion

        #region Value Extension
        public static IEnumerable<IResolutionContext<T>> All<T>(this ITestingContext context, string name)
        {
            return context.All(context.GetToken<T>(name));
        }

        public static T Value<T>(this ITestingContext context, IToken<T> token)
        {
            return context.All(token).Select(x => x.Value).FirstOrDefault();
        }

        public static T Value<T>(this ITestingContext context, string name)
        {
            return context.All(context.GetToken<T>(name)).Select(x => x.Value).FirstOrDefault();
        }
        #endregion
    }
}
