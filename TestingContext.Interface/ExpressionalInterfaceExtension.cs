namespace TestingContextInterface
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using TestingContextLimitedInterface.Diag;
    using TestingContextLimitedInterface.Tokens;

    public static class ExpressionalInterfaceExtension
    {
        #region IRegister

        public static void Exists<T1>(this IRegister register,
            string name,
            Expression<Func<IEnumerable<T1>>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = DiagInfoExpressionFactory.CreateDiag(file, line, member, srcFunc);
            register.Exists(diag, name, srcFunc.Compile());
        }

        #endregion

        #region IFor

        public static IFilterToken IsTrue<T1>(this IFor<T1> ifor,
            Expression<Func<T1, bool>> filter,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = DiagInfoExpressionFactory.CreateDiag(file, line, member, filter);
            return ifor.IsTrue(diag, filter.Compile());
        }

        public static void Exists<T1, T2>(this IFor<T1> ifor,
            string name,
            Expression<Func<T1, IEnumerable<T2>>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = DiagInfoExpressionFactory.CreateDiag(file, line, member, srcFunc);
            ifor.Exists(diag, name, srcFunc.Compile());
        }

        public static void DoesNotExist<T1, T2>(this IFor<T1> ifor,
            string name,
            Expression<Func<T1, IEnumerable<T2>>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = DiagInfoExpressionFactory.CreateDiag(file, line, member, srcFunc);
            ifor.DoesNotExist(diag, name, srcFunc.Compile());
        }

        public static void Each<T1, T2>(this IFor<T1> ifor,
            string name,
            Expression<Func<T1, IEnumerable<T2>>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = DiagInfoExpressionFactory.CreateDiag(file, line, member, srcFunc);
            ifor.Each(diag, name, srcFunc.Compile());
        }

        public static void Is<T1, T2>(this IFor<T1> ifor,
            string name,
            Expression<Func<T1, T2>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = DiagInfoExpressionFactory.CreateDiag(file, line, member, srcFunc);
            ifor.Exists(diag, name, TestingContextLimitedInterface.ExpressionalInterfaceExtension.SingleFunc(srcFunc));
        }

        public static void IsNot<T1, T2>(this IFor<T1> ifor,
            string name,
            Expression<Func<T1, T2>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = DiagInfoExpressionFactory.CreateDiag(file, line, member, srcFunc);
            ifor.DoesNotExist(diag, name, TestingContextLimitedInterface.ExpressionalInterfaceExtension.SingleFunc(srcFunc));
        }

        #endregion

        #region IFor2

        public static IFilterToken IsTrue<T1, T2>(this IFor<T1, T2> ifor,
            Expression<Func<T1, T2, bool>> filter,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = DiagInfoExpressionFactory.CreateDiag(file, line, member, filter);
            return ifor.IsTrue(diag, filter.Compile());
        }

        public static void Exists<T1, T2, T3>(this IFor<T1, T2> ifor,
            string name,
            Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = DiagInfoExpressionFactory.CreateDiag(file, line, member, srcFunc);
            ifor.Exists(diag, name, srcFunc.Compile());
        }

        public static void DoesNotExist<T1, T2, T3>(this IFor<T1, T2> ifor,
            string name,
            Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = DiagInfoExpressionFactory.CreateDiag(file, line, member, srcFunc);
            ifor.DoesNotExist(diag, name, srcFunc.Compile());
        }

        public static void Each<T1, T2, T3>(this IFor<T1, T2> ifor,
            string name,
            Expression<Func<T1, T2, IEnumerable<T3>>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = DiagInfoExpressionFactory.CreateDiag(file, line, member, srcFunc);
            ifor.Each(diag, name, srcFunc.Compile());
        }

        public static void Is<T1, T2, T3>(this IFor<T1, T2> ifor,
            string name,
            Expression<Func<T1, T2, T3>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = DiagInfoExpressionFactory.CreateDiag(file, line, member, srcFunc);
            ifor.Exists(diag, name, TestingContextLimitedInterface.ExpressionalInterfaceExtension.SingleFunc(srcFunc));
        }

        public static void IsNot<T1, T2, T3>(this IFor<T1, T2> ifor,
            string name,
            Expression<Func<T1, T2, T3>> srcFunc,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string member = "")
        {
            var diag = DiagInfoExpressionFactory.CreateDiag(file, line, member, srcFunc);
            ifor.DoesNotExist(diag, name, TestingContextLimitedInterface.ExpressionalInterfaceExtension.SingleFunc(srcFunc));
        }
        #endregion
    }
}
