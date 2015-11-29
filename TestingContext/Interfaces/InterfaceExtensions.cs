namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using TestingContextCore.Interfaces.Register;
    using TestingContextCore.Interfaces.Tokens;

    public static class InterfaceExtensions
    {
        public static IRegister Not(this IRegister register)
        {
            IRegister output = null;
            register.Not(x => output = x);
            return output;
        }

        #region Value Extension
        public static T Value<T>(this ITestingContext context, IToken<T> token)
            => context.All(token).Select(x => x.Value).FirstOrDefault();
        public static T Value<T>(this ITestingContext context, string name)
            => context.All<T>(name).Select(x => x.Value).FirstOrDefault();
        #endregion

        #region IFor1
        public static IToken<T2> Is<T1, T2>(this IFor<T1> ifor,
            Func<T1, T2> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "") => ifor.Exists(ItemFunc(srcFunc), file, line, member);

        public static IToken<T2> IsNot<T1, T2>(this IFor<T1> ifor, 
            Func<T1, T2> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "") => ifor.DoesNotExist(ItemFunc(srcFunc), file, line, member);

        public static void Is<T1, T2>(this IFor<T1> ifor, 
            string name,
            Func<T1, T2> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "") => ifor.Exists(name, ItemFunc(srcFunc), file, line, member);

        public static void IsNot<T1, T2>(this IFor<T1> ifor, 
            string name,
            Func<T1, T2> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "") => ifor.DoesNotExist(name, ItemFunc(srcFunc), file, line, member);

        private static Func<T1, IEnumerable<T2>> ItemFunc<T1, T2>(Func<T1, T2> srcFunc)
        {
            return x =>
            {
                var item = srcFunc(x);
                return item == null ? Enumerable.Empty<T2>() : new[] { item };
            };
        }
        #endregion

        #region IFor2
        public static IToken<T3> Is<T1, T2, T3>(this IFor<T1, T2> ifor,
            Func<T1, T2, T3> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "") => ifor.Exists(ItemFunc(srcFunc), file, line, member);

        public static IToken<T3> IsNot<T1, T2, T3>(this IFor<T1, T2> ifor, 
            Func<T1, T2, T3> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "") => ifor.DoesNotExist(ItemFunc(srcFunc), file, line, member);


        public static void Is<T1, T2, T3>(this IFor<T1, T2> ifor, 
            string name,
            Func<T1, T2, T3> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "") => ifor.Exists(name, ItemFunc(srcFunc), file, line, member);

        public static void IsNot<T1, T2, T3>(this IFor<T1, T2> ifor, 
            string name,
            Func<T1, T2, T3> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "") => ifor.DoesNotExist(name, ItemFunc(srcFunc), file, line, member);

        private static Func<T1, T2, IEnumerable<T3>> ItemFunc<T1, T2, T3>(Func<T1, T2, T3> srcFunc)
        {
            return (x, y) =>
            {
                var item = srcFunc(x, y);
                return item == null ? Enumerable.Empty<T3>() : new[] { item };
            };
        }
        #endregion
    }
}
