namespace TestingContextCore.Implementation.Registration
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Filters;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;

    internal class Registration : IRegister
    {
        private readonly TokenStore store;
        private readonly IFilterGroup group;

        public Registration(TokenStore store, IFilterGroup group = null)
        {
            this.store = store;
            this.group = group;
        }

        public void Not(Action<IRegister> action)
        { }

        public void Or(Action<IRegister> action, 
            Action<IRegister> action2, 
            Action<IRegister> action3 = null, 
            Action<IRegister> action4 = null, 
            Action<IRegister> action5 = null, 
            string file = "", 
            int line = 0, 
            string member = "")
        { }

        public void Xor(Action<IRegister> action, 
            Action<IRegister> action2, 
            string file = "", 
            int line = 0, 
            string member = "")
        { }

        public IFor<T> For<T>(Func<ITestingContext, IToken<T>> getToken)
        {
            return null;
        }

        public IFor<IEnumerable<T>> ForCollection<T>(Func<ITestingContext, IToken<T>> getToken)
        {
            return null;
        }

        public IHaveToken<T> Exists<T>(Func<IEnumerable<T>> srcFunc)
        {
            return null;
        }

        public IHaveToken<T> Is<T>(Func<T> srcFunc)
        {
            return null;
        }
    }
}
