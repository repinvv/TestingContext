namespace TestingContextCore
{
    using System;

    public interface IFor<T1>
    {
        void Filter(Func<T1, bool> filter);
        IFor<T1, T2> For<T2>(string key);
    }

    public interface IFor<T1, T2>
    {
        void Filter(Func<T1, T2, bool> filter);
        IFor<T1, T2, T3> For<T3>(string key);
    }

    public interface IFor<T1, T2, T3>
    {
        void Filter(Func<T1, T2, T3, bool> filter);
        IFor<T1, T2, T3, T4> For<T4>(string key);
    }

    public interface IFor<T1, T2, T3, T4>
    {
        void Filter(Func<T1, T2, T3, T4, bool> filter);
    }
}
