namespace TestingContextCore.Implementation.Registrations.Registration1
{
    using System;
    using System.Collections.Generic;
    using TestingContextCore.Implementation.Tokens;
    using TestingContextCore.Interfaces;
    using TestingContextCore.Interfaces.Tokens;

    internal partial class Registration1<T1> : IFor<T1>
    {
        private readonly TokenStore store;
        private readonly InnerRegistration1<T1> inner;

        public Registration1(TokenStore store, InnerRegistration1<T1> inner)
        {
            this.store = store;
            this.inner = inner;
        }

        public IFor<T1, T2> For<T2>(Func<IToken<T2>> getToken) => inner.For(new LazyHaveToken<T2>(getToken));

        public IFor<T1, IEnumerable<T2>> ForCollection<T2>(Func<IToken<T2>> getToken) => inner.ForCollection(new LazyHaveToken<T2>(getToken));


        //public ISaveToken<T2> Exists<T2>(Func<T1, IEnumerable<T2>> srcFunc, int line, string file, string member) 
        //    => inner.CreateDefinition(srcFunc, x => x.Any(y => y.MeetsConditions), Parent, file, line, member);
        //public ISaveToken<T2> DoesNotExist<T2>(Func<T1, IEnumerable<T2>> srcFunc, int line, string file, string member) 
        //    => inner.CreateDefinition(srcFunc, x => !x.Any(y => y.MeetsConditions), Parent, file, line, member);
        //public ISaveToken<T2> Each<T2>(Func<T1, IEnumerable<T2>> srcFunc, int line, string file, string member)
        //    => inner.CreateDefinition(srcFunc,
        //                        x => x.GroupBy(item => item).All(grp => grp.Any(item => item.MeetsConditions)),
        //                        SourceParent, file, line, member);


        //#region named
        //public void Exists<T2>(Func<T1, IEnumerable<T2>> srcFunc, string name, int line, string file, string member)
        //    => Exists(srcFunc, line, file, member).SaveAs(name);
        //public void DoesNotExist<T2>(Func<T1, IEnumerable<T2>> srcFunc, string name, int line, string file, string member) 
        //    => DoesNotExist(srcFunc, line, file, member).SaveAs(name);
        //public void Each<T2>(Func<T1, IEnumerable<T2>> srcFunc, string name, int line, string file, string member) 
        //    => Each(srcFunc, line, file, member).SaveAs(name);
        //#endregion

    }
}
