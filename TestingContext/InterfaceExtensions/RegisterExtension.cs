namespace TestingContextCore.InterfaceExtensions
{
    using System.Collections.Generic;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;

    public static class RegisterExtension
    {
        public static IRegister Not(this IRegister register)
        {
            IRegister output = null;
            register.Not(x => output = x);
            return output;
        }

        public static IFor<T> For<T>(this IRegister register, IHaveToken<T> haveToken)
        {
            return register.For(x => haveToken.Token);
        }

        public static IFor<IEnumerable<T>> ForCollection<T>(this IRegister register, IHaveToken<T> haveToken)
        {
            return register.ForCollection(x => haveToken.Token);
        }

        public static IFor<T1, T2> For<T1, T2>(this IFor<T1> ifor, IHaveToken<T2> haveToken)
        {
            return ifor.For(x => haveToken.Token);
        }

        public static IFor<T1, IEnumerable<T2>> ForCollection<T1, T2>(this IFor<T1> ifor, IHaveToken<T2> haveToken)
        {
            return ifor.ForCollection(x => haveToken.Token);
        }
    }
}
