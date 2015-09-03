namespace TestingContextCore
{
    using System;

    internal class ForImpl1<T1> : IFor<T1>
    {
        private readonly string key1;
        private readonly ContextCore core;

        public ForImpl1(string key1, ContextCore core)
        {
            this.key1 = key1;
            this.core = core;
        }

        public void Filter(Func<T1, bool> filter)
        {
            throw new NotImplementedException();
        }

        public IFor<T1, T2> For<T2>(string key2)
        {
            return new ForImpl2<T1, T2>(key1, key2, core);
        }
    }

    internal class ForImpl2<T1, T2> : IFor<T1, T2>
    {
        private readonly string key1;
        private readonly string key2;
        private readonly ContextCore core;

        public ForImpl2(string key1, string key2, ContextCore core)
        {
            this.key1 = key1;
            this.key2 = key2;
            this.core = core;
        }

        public void Filter(Func<T1, T2, bool> filter)
        {
            throw new NotImplementedException();
        }

        public IFor<T1, T2, T3> For<T3>(string key3)
        {
            return new ForImpl3<T1, T2, T3>(key1, key2, key3, core);
        }
    }

    internal class ForImpl3<T1, T2, T3> : IFor<T1, T2, T3>
    {
        private readonly string key1;
        private readonly string key2;
        private readonly string key3;
        private readonly ContextCore core;

        public ForImpl3(string key1, string key2, string key3, ContextCore core)
        {
            this.key1 = key1;
            this.key2 = key2;
            this.key3 = key3;
            this.core = core;
        }

        public void Filter(Func<T1, T2, T3, bool> filter)
        {
            throw new NotImplementedException();
        }

        public IFor<T1, T2, T3, T4> For<T4>(string key4)
        {
            return new ForImpl4<T1, T2, T3, T4>(key1, key2, key3, key4, core);
        }
    }

    internal class ForImpl4<T1, T2, T3, T4> : IFor<T1, T2, T3, T4>
    {
        private readonly string key1;
        private readonly string key2;
        private readonly string key3;
        private readonly ContextCore core;

        public ForImpl4(string key1, string key2, string key3, string key4, ContextCore core)
        {
            this.key1 = key1;
            this.key2 = key2;
            this.key3 = key3;
            this.core = core;
        }

        public void Filter(Func<T1, T2, T3, T4, bool> filter)
        {
            throw new NotImplementedException();
        }
    }
}
