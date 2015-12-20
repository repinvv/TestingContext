namespace TestingContext.LimitedInterface
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using TestingContext.LimitedInterface.Tokens;
    using static Diag.DiagInfoExpressionFactory;

    public static class ExpressionalInterfaceExtension
    {
        #region IForToken

        public static IFilterToken IsTrue<T1>(this IForToken<T1> ifor,
            Expression<Func<T1, bool>> filter,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = CreateDiag(file, line, member, filter);
            return ifor.IsTrue(diag, filter.Compile());
        }

        public static IHaveToken<T2> Exists<T1, T2>(this IForToken<T1> ifor,
            Expression<Func<T1, IEnumerable<T2>>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = CreateDiag(file, line, member, srcFunc);
            return ifor.Exists(diag, srcFunc.Compile());
        }

        public static IHaveToken<T2> DoesNotExist<T1, T2>(this IForToken<T1> ifor,
            Expression<Func<T1, IEnumerable<T2>>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = CreateDiag(file, line, member, srcFunc);
            return ifor.DoesNotExist(diag, srcFunc.Compile());
        }

        public static IHaveToken<T2> Each<T1, T2>(this IForToken<T1> ifor,
            Expression<Func<T1, IEnumerable<T2>>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = CreateDiag(file, line, member, srcFunc);
            return ifor.Each(diag, srcFunc.Compile());
        }

        public static IHaveToken<T2> Is<T1, T2>(this IForToken<T1> ifor,
            Expression<Func<T1, T2>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = CreateDiag(file, line, member, srcFunc);
            return ifor.Exists(diag, SingleFunc(srcFunc));
        }

        public static IHaveToken<T2> IsNot<T1, T2>(this IForToken<T1> ifor,
            Expression<Func<T1, T2>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = CreateDiag(file, line, member, srcFunc);
            return ifor.DoesNotExist(diag, SingleFunc(srcFunc));
        }

        public static Func<T1, IEnumerable<T2>> SingleFunc<T1, T2>(Expression<Func<T1, T2>> src)
        {
            var compiled = src.Compile();
            return x => new[] { compiled(x) };
        }

        #endregion

        #region IForToken2

        public static IFilterToken IsTrue<T1, T2>(this IForToken<T1, T2> ifor,
            Expression<Func<T1, T2, bool>> filter,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = CreateDiag(file, line, member, filter);
            return ifor.IsTrue(diag, filter.Compile());
        }

        public static IHaveToken<T3> Exists<T1, T2, T3>(this IForToken<T1, T2> ifor,
            Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = CreateDiag(file, line, member, srcFunc);
            return ifor.Exists(diag, srcFunc.Compile());
        }

        public static IHaveToken<T3> DoesNotExist<T1, T2, T3>(this IForToken<T1, T2> ifor,
            Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = CreateDiag(file, line, member, srcFunc);
            return ifor.DoesNotExist(diag, srcFunc.Compile());
        }

        public static IHaveToken<T3> Each<T1, T2, T3>(this IForToken<T1, T2> ifor,
            Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = CreateDiag(file, line, member, srcFunc);
            return ifor.Each(diag, srcFunc.Compile());
        }

        public static IHaveToken<T3> Is<T1, T2, T3>(this IForToken<T1, T2> ifor,
            Expression<Func<T1, T2, T3>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = CreateDiag(file, line, member, srcFunc);
            return ifor.Exists(diag, SingleFunc(srcFunc));
        }

        public static IHaveToken<T3> IsNot<T1, T2, T3>(this IForToken<T1, T2> ifor,
            Expression<Func<T1, T2, T3>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = CreateDiag(file, line, member, srcFunc);
            return ifor.DoesNotExist(diag, SingleFunc(srcFunc));
        }

        public static Func<T1, T2, IEnumerable<T3>> SingleFunc<T1, T2, T3>(Expression<Func<T1, T2, T3>> src)
        {
            var compiled = src.Compile();
            return (x, y) => new[] { compiled(x, y) };
        }

        #endregion
    }
}
