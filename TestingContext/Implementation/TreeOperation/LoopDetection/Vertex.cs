namespace TestingContextCore.Implementation.TreeOperation.LoopDetection
{
    using System.Collections.Generic;

    internal class Vertex<T> 
        where T: Vertex<T>
    {
        public int Index { get; set; } = -1;

        public int LowLink { get; set; }

        public List<T> Dependencies { get; } = new List<T>();
    }
}