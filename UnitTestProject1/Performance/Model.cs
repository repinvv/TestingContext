namespace UnitTestProject1.Performance
{
    using System.Collections.Generic;

    public class Item
    {
        public bool Valid { get; set; }
    }

    public class Model
    {
        public Dictionary<int, List<Item>> Branches { get; set; }
    }
}
