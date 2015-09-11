namespace TestingContextCore.Implementation
{
    using System;

    internal class Definition
    {
        public Definition(Type type, string key)
        {
            Type = type;
            Key = key;
        }

        public Type Type { get; set; }

        public string Key { get; set; }

        public bool Is(Type type, string key)
        {
            return Type == type && key == Key;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Definition);
        }

        protected bool Equals(Definition other)
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

        public override string ToString()
        {
            return Type.Name + $" \"{Key}\"";
        }
    }
}
