namespace TestingContextCore.Implementation
{
    using System;

    internal class EntityDefinition
    {
        public EntityDefinition(Type type, string key)
        {
            Type = type;
            Key = key;
        }

        public Type Type { get; set; }

        public string Key { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as EntityDefinition);
        }

        protected bool Equals(EntityDefinition other)
        {
            return Type == other.Type 
                && string.Equals(Key, other.Key);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Type.GetHashCode() * 397) ^ Key.GetHashCode();
            }
        }

    }
}
