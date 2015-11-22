namespace TestingContextCore.Implementation.Registration
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;
    using TestingContextCore.PublicMembers;

    internal class Registration2<T1, T2> : IFor<T1, T2>
    {
        public IHaveToken IsTrue(Expression<Func<T1, T2, bool>> filter, string file = "", int line = 0, string member = "")
        {
            var diagInfo = new DiagInfo(file, line, member, filter);
            return null;
        }

        #region unnamed
        public IHaveToken<T3> Exists<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc)
        {
            return null;
        }

        public IHaveToken<T3> DoesNotExist<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc)
        {
            return null;
        }

        public IHaveToken<T3> Each<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc)
        {
            return null;
        }

        public IHaveToken<T3> Is<T3>(Func<T1, T2, T3> srcFunc)
        {
            return null;
        }

        public IHaveToken<T3> IsNot<T3>(Func<T1, T2, T3> srcFunc)
        {
            return null;
        }
        #endregion

        #region named
        public void Exists<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string name) => Exists(srcFunc).SaveAs(name);
        public void DoesNotExist<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string name) => DoesNotExist(srcFunc).SaveAs(name);
        public void Each<T3>(Func<T1, T2, IEnumerable<T3>> srcFunc, string name) => Each(srcFunc).SaveAs(name);
        public void Is<T3>(Func<T1, T2, T3> srcFunc, string name) => Is(srcFunc).SaveAs(name);
        public void IsNot<T3>(Func<T1, T2, T3> srcFunc, string name) => IsNot(srcFunc).SaveAs(name);
        #endregion
    }
}
