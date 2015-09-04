namespace TestingContextCore.Implementation
{
    using System.Collections.Generic;

    public static class DictionaryExtension
    {
        public static List<T2> GetList<T1, T2>(this Dictionary<T1, List<T2>> dict, T1 key)
        {
            List<T2> list;
            if (dict.TryGetValue(key, out list))
            {
                return list;
            }

            list = new List<T2>();
            dict.Add(key, list);
            return list;
        }
    }
}
