namespace TestingContextCore.Implementation
{
    using System;

    internal class Definition : IEquatable<Definition>
    {
        public static Definition Define<T>(string key, Definition scope)
        {
            return new Definition(typeof(T), (key ?? string.Empty).Trim(), scope);
        }

        private Definition(Type type, string key, Definition scope)
        {
            Type = type;
            Key = key;
            Scope = scope;
        }

        public Type Type { get; }

        public string Key { get; }

        public Definition Scope { get; }

        #region Equal members
        public override bool Equals(object obj)
        {
            return Equals(obj as Definition);
        }

        public bool Equals(Definition other)
        {
            return other != null
                && Type == other.Type 
                && Key == other.Key
                && Scope == other.Scope;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Type?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (Key?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Scope?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            var key = string.IsNullOrEmpty(Key) ? string.Empty : (" " + Key);
            var scope = Scope == null ? string.Empty : (" : " + Scope);
            return Type.Name + key + scope;
        }

        public static bool operator ==(Definition definition, Definition other)
        {
            return ReferenceEquals(definition, other)
                || (!ReferenceEquals(definition, null) && definition.Equals(other));
        }

        public static bool operator !=(Definition definition, Definition other)
        {
            return !(definition == other);
        }
        #endregion
    }
}
