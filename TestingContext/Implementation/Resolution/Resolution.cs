using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections;
    using TestingContextCore.Interfaces;

    internal abstract class Resolution<T> : IResolution<T>, IResolution
    {
        private readonly Definition definition;
        

        protected Resolution(Definition definition)
        {
            this.definition = definition;
        }

        IEnumerator<IResolutionContext> IEnumerable<IResolutionContext>.GetEnumerator() => (Source as IEnumerable<IResolutionContext>).GetEnumerator();

        IEnumerator<IResolutionContext<T>> IEnumerable<IResolutionContext<T>>.GetEnumerator() => Source.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Source.GetEnumerator();

        public IResolution Resolve(Definition definition)
        {
            return this.definition.Equals(definition) ? this : null;
        }

        public abstract bool MeetsConditions { get; }

        protected IEnumerable<IResolutionContext<T>> Source { get; set; }
    }
}
