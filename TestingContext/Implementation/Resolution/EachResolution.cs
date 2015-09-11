namespace TestingContextCore.Implementation.Resolution
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using TestingContextCore.CachingEnumerable;
    using TestingContextCore.Interfaces;

    internal class EachResolution<T> : Resolution<T>
    {

        public EachResolution(IEnumerable<IResolutionContext<T>> source, Definition definition)
            : base(definition)
        {
            var list = source.ToArray();
            //MeetsConditions = list.All(x => (x as IResolutionContext).MeetsConditions);
            Source = list;
        }

        public override bool MeetsConditions { get; }
    }
    
}
