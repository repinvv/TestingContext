namespace TestingContextInterface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using TestingContextLimitedInterface.Tokens;

    public static class InterfaceExtension
    {
        #region IMatcher

        public static IEnumerable<IResolutionContext<T>> All<T>(this IMatcher matcher, IHaveToken<T> token)
            => matcher.All(token.Token);

        public static T Value<T>(this IMatcher matcher, IHaveToken<T> token)
            => matcher.Value(token.Token);

        public static T Value<T>(this IMatcher matcher, IToken<T> token)
            => matcher.All(token)
                      .Select(x => x.Value)
                      .FirstOrDefault();

        public static T Value<T>(this IMatcher matcher, string name)
            => matcher.All<T>(name)
                      .Select(x => x.Value)
                      .FirstOrDefault();

        public static IEnumerable<Tuple<IFailure, T>> Candidates<T>(this IMatcher matcher, IHaveToken<T> token)
            => matcher.Candidates(token.Token);

        public static IEnumerable<T> BestCandidates<T>(this IMatcher matcher, IHaveToken<T> token)
            => matcher.BestCandidates(token.Token);

        public static IEnumerable<T> BestCandidates<T>(this IMatcher matcher, string name)
            => matcher.Candidates<T>(name)
                      .Where(x => x.Item1 == matcher.GetFailure())
                      .Select(x => x.Item2);

        public static IEnumerable<T> BestCandidates<T>(this IMatcher matcher, IToken<T> token)
            => matcher.Candidates(token)
                      .Where(x => x.Item1 == matcher.GetFailure())
                      .Select(x => x.Item2);
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
