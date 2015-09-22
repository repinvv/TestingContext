namespace TestingContextCore.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IFor<T1>
    {
        void Filter(Func<T1, bool> filter);

        IWith<T1, T2> With<T2>(string key);

        IWith<T1, IEnumerable<IResolutionContext<T2>>> WithCollection<T2>(string key);
    }

    public interface IWith<T1, T2>
    {
        void Filter(Func<T1, T2, bool> filter);
    }
}
