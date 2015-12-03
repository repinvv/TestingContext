namespace UnitTestProject1.Performance
{
    using System.Collections.Generic;
    using System.Linq;

    internal static class ModelGenerator
    {
        public static IEnumerable<Model> GenerateModels(int branches, int items)
        {
            return Enumerable.Range(0, branches).Select(x => new Model { Branches = GetBranches(branches, items, x) });
        }

        private static Dictionary<int, List<Item>> GetBranches(int branches, int items, int faultyBranch)
        {
            return Enumerable.Range(0, branches).ToDictionary(x => x, x => GetItems(items, faultyBranch != x));
        }

        private static List<Item> GetItems(int items, bool valid)
        {
            return Enumerable.Range(0, items).Select(x => new Item { Valid = valid }).ToList();
        }
    }
}
