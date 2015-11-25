namespace TestingContextCore.PublicMembers
{
    using System.Collections.Generic;
    using TestingContextCore.Implementation;
    using TestingContextCore.PublicMembers.Exceptions;

    public class Storage
    {
        private readonly Dictionary<Definition, object> store = new Dictionary<Definition, object>();

        public void Set<T>(T value, string key = null)
        {
            store.Add(Definition.Define<T>(key), value);
        }

        public T Get<T>(string key = null)
            where T : class
        {
            object value;
            var definition = Definition.Define<T>(key);
            store.TryGetValue(definition, out value);
            return value as T;
        }
    }
}
