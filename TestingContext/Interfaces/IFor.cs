namespace TestingContextCore.Interfaces
{
    using System;

    public interface IFor<T1>
    {
        void Filter(Func<T1, bool> filter);
        IWith<T1, T2> With<T2>(string key);
    }

    public interface IWith<T1, T2>
    {
        void Filter(Func<T1, T2, bool> filter);
        ////IWith<T1, T2, T3> With<T3>(string key);
    }

    ////public interface IWith<T1, T2, T3>
    ////{
    ////    void Filter(Func<T1, T2, T3, bool> filter);
    ////    IWith<T1, T2, T3, T4> With<T4>(string key);
    ////}

    ////public interface IFor<T1, T2, T3, T4>
    ////{
    ////    void Filter(Func<T1, T2, T3, T4, bool> filter);
    ////}
}
