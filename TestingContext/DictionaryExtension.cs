namespace TestingContextCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DictionaryExtension
    {
        /// <summary>
        /// This method exists because of bogus syntax of "out".
        /// It forces you to write bulky code for simple operation
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue SafeGet<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            if (key == null)
            {
                return default(TValue);
            }

            TValue value;
            return dict.TryGetValue(key, out value) ? value : default(TValue);
        }

        public static TValue SafeGet<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defVal)
        {
            if (key == null)
            {
                return default(TValue);
            }

            TValue value;
            return dict.TryGetValue(key, out value) ? value : defVal;
        }

        public static TValue SafeGet<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TValue> defValSelector)
        {
            if (key == null)
            {
                return default(TValue);
            }
            TValue value;
            return dict.TryGetValue(key, out value) ? value : defValSelector();
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TValue> defValSelector)
        {
            if (key == null)
            {
                return default(TValue);
            }
            TValue value;
            if (dict.TryGetValue(key, out value))
            {
                return value;
            }

            value = defValSelector();
            dict.Add(key, value);
            return value;
        }

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

        /// <summary>
        /// Regular ToList method forces creation of new list object,
        /// AsList will first try to cast it to list, and only then will make new list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<T> AsList<T>(this IEnumerable<T> input)
        {
            return input as List<T> ?? input.ToList();
        }
    }
}