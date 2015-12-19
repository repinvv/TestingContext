namespace TestingContextCore.PublicMembers
{
    using System.Collections.Generic;
    using TestingContext.Interface;
    using TestingContextCore.Implementation;

    public class Storage : IStorage
    {
        private readonly Dictionary<Definition, object> store = new Dictionary<Definition, object>();

        public void Set<T>(T value, string key = null)
        {
            store[Definition.Define<T>(key)] = value;
        }

        public T Get<T>(string key = null)
        {
            object value;
            store.TryGetValue(Definition.Define<T>(key), out value);
            return (T)value;
        }
    }
}
