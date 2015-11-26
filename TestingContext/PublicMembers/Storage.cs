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
            store[Definition.Define<T>(key)] = value;
        }

        public T Get<T>(string key = null)
            where T : class
        {
            object value;
            store.TryGetValue(Definition.Define<T>(key), out value);
            return value as T;
        }
    }
}
