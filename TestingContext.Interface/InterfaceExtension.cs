namespace TestingContext.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using TestingContext.LimitedInterface.Diag;
    using TestingContext.LimitedInterface.Tokens;

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

        #region ITestingContext
        public static IHaveToken<T> GetToken<T>(this ITestingContext context,
                                                string name,
                                                [CallerFilePath] string file = "",
                                                [CallerLineNumber] int line = 0,
                                                [CallerMemberName] string member = "")
        {
            var diag = DiagInfo.Create(file, line, member);
            return context.GetToken<T>(diag, name);
        }

        public static void SetToken<T>(this ITestingContext context,
                                       string name,
                                       IHaveToken<T> haveToken,
                                       [CallerFilePath] string file = "",
                                       [CallerLineNumber] int line = 0,
                                       [CallerMemberName] string member = "")
        {
            var diag = DiagInfo.Create(file, line, member);
            context.SetToken(diag, name, haveToken);
        }
        #endregion

        #region IRegister
        public static IFor<T> For<T>(this IRegister register,
                                     string name,
                                     [CallerFilePath] string file = "",
                                     [CallerLineNumber] int line = 0,
                                     [CallerMemberName] string member = "")
        {
            var diag = DiagInfo.Create(file, line, member);
            return register.For<T>(diag, name);
        }

        public static IFor<IEnumerable<T>> ForCollection<T>(this IRegister register,
                                                            string name,
                                                            [CallerFilePath] string file = "",
                                                            [CallerLineNumber] int line = 0,
                                                            [CallerMemberName] string member = "")
        {
            var diag = DiagInfo.Create(file, line, member);
            return register.ForCollection<T>(diag, name);
        }

        public static IFilterToken Not(this IRegister register, 
            Action<IRegister> action,
                                                            [CallerFilePath] string file = "",
                                                            [CallerLineNumber] int line = 0,
                                                            [CallerMemberName] string member = "")
        {
            var diag = DiagInfo.Create(file, line, member);
            return register.Not(diag, action);
        }

        public static IRegister Not(this IRegister register,
                            [CallerFilePath] string file = "",
                            [CallerLineNumber] int line = 0,
                            [CallerMemberName] string member = "")
        {
            var diag = DiagInfo.Create(file, line, member);
            IRegister output = null;
            register.Not(diag, x => output = x);
            return output;
        }

        public static IFilterToken Either(this IRegister register,
                                          Action<IRegister> action,
                                          Action<IRegister> action2,
                                          Action<IRegister> action3 = null,
                                          Action<IRegister> action4 = null,
                                          Action<IRegister> action5 = null,
                                          [CallerFilePath] string file = "",
                                          [CallerLineNumber] int line = 0,
                                          [CallerMemberName] string member = "")
        {
            var diag = DiagInfo.Create(file, line, member);
            var actions = LimitedInterface.InterfaceExtensions.Actions(action, action2, action3, action4, action5);
            return register.Either(diag, actions);
        }

        public static IFilterToken Xor(this IRegister register, 
            Action<IRegister> action, 
            Action<IRegister> action2,
                                                            [CallerFilePath] string file = "",
                                                            [CallerLineNumber] int line = 0,
                                                            [CallerMemberName] string member = "")
        {
            var diag = DiagInfo.Create(file, line, member);
            return register.Xor(diag, action, action2);
        }
        #endregion

        #region IFor

        public  static IFilterToken Not<T>(this IFor<T> ifor, 
            Action<IRegister> action,
                                          [CallerFilePath] string file = "",
                                          [CallerLineNumber] int line = 0,
                                          [CallerMemberName] string member = "")
        {
            var diag = DiagInfo.Create(file, line, member);
            return ifor.Not(diag, action);
        }

        public static IFilterToken Either<T>(this IFor<T> ifor,
            Action<IRegister> action,
                                          Action<IRegister> action2,
                                          Action<IRegister> action3 = null,
                                          Action<IRegister> action4 = null,
                                          Action<IRegister> action5 = null,
                                          [CallerFilePath] string file = "",
                                          [CallerLineNumber] int line = 0,
                                          [CallerMemberName] string member = "")
        {
            var diag = DiagInfo.Create(file, line, member);
            var actions = LimitedInterface.InterfaceExtensions.Actions(action, action2, action3, action4, action5);
            return ifor.Either(diag, actions);
        }

        public static IFilterToken Xor<T>(this IFor<T> ifor,
                                          Action<IRegister> action,
                                          Action<IRegister> action2,
                                          [CallerFilePath] string file = "",
                                          [CallerLineNumber] int line = 0,
                                          [CallerMemberName] string member = "")
        {
            var diag = DiagInfo.Create(file, line, member);
            return ifor.Xor(diag, action, action2);
        }
        #endregion
    }
}
