namespace TestingContextCore.Implementation
{
    using System;

    internal class Definition : IEquatable<Definition>
    {
        public static Definition Define<T>(string key)
        {
            return new Definition(typeof(T), (key ?? string.Empty).Trim());
        }

        private Definition(Type type, string key)
        {
            Type = type;
            Key = key;
        }

        public Type Type { get; }

        public string Key { get; }

        public override bool Equals(object obj)
        {
            return (obj is Definition) && Equals((Definition)obj);
        }

        public bool Equals(Definition other)
        {
            return Type == other.Type 
                && Key == other.Key;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Type?.GetHashCode() ?? 0 * 397) ^ Key?.GetHashCode() ?? 0;
            }
        }

        public override string ToString()
        {
            return Type.Name + $" \"{Key}\"";
        }

        public static bool operator ==(Definition definition, Definition other)
        {
            return definition.Equals(other);
        }

        public static bool operator !=(Definition definition, Definition other)
        {
            return !definition.Equals(other);
        }
    }
}
