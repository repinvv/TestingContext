namespace TestingContextCore.Implementation.TreeOperation.LoopDetection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class TarjanLoopDetection<T>
        where T : Vertex<T>
    {
        private readonly List<List<T>> stronglyConnected = new List<List<T>>();
        private readonly Stack<T> stack = new Stack<T>();
        private int index;

        public List<List<T>> DetectLoop(ICollection<T> vertices)
        {
            foreach (var v in vertices.Where(v => v.Index < 0))
            {
                StrongConnect(v);
            }

            return stronglyConnected;
        }

        private void StrongConnect(T v)
        {
            v.Index = index;
            v.LowLink = index;
            index++;
            stack.Push(v);

            foreach (var w in v.Dependencies)
            {
                if (w.Index < 0)
                {
                    StrongConnect(w);
                    v.LowLink = Math.Min(v.LowLink, w.LowLink);
                }
                else if (stack.Contains(w))
                {
                    v.LowLink = Math.Min(v.LowLink, w.Index);
                }
            }

            if (v.LowLink == v.Index)
            {
                var scc = new List<T>();
                T w;
                do
                {
                    w = stack.Pop();
                    scc.Add(w);
                } while (v != w);
                stronglyConnected.Add(scc);
            }
        }
    }
}
