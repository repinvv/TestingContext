namespace TestingContextInterface
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using TestingContextLimitedInterface.Tokens;

    public static class InterfaceExtension
    {
        #region IMatcher

        public static T Value<T>(this IMatcher context, IToken<T> token)
            => context.All(token)
                      .Select(x => x.Value)
                      .FirstOrDefault();

        public static T Value<T>(this IMatcher context, string name)
            => context.All<T>(name)
                      .Select(x => x.Value)
                      .FirstOrDefault();

        public static IEnumerable<T> BestCandidates<T>(this IMatcher matcher, string name)
        {
            return matcher.Candidates<T>(name)
                          .Where(x => x.Item1 == matcher.GetFailure())
                          .Select(x => x.Item2);
        }

        public static IEnumerable<T> BestCandidates<T>(this IMatcher matcher, IToken<T> token)
        {
            return matcher.Candidates(token)
                          .Where(x => x.Item1 == matcher.GetFailure())
                          .Select(x => x.Item2);
        }

        #endregion

        #region IRegister

        public static IRegister Not(this IRegister register,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            IRegister output = null;
            register.Not(x => output = x, file, line, member);
            return output;
        }

        public static IRegister Not<T>(this IFor<T> ifor,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            IRegister output = null;
            ifor.Not(x => output = x);
            return output;
        }

        public static IRegister Not<T1, T2>(this IFor<T1, T2> ifor,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            IRegister output = null;
            ifor.Not(x => output = x);
            return output;
        }

        #endregion
    }
}
