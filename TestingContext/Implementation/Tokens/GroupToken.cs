﻿namespace TestingContextCore.Implementation.Tokens
{
    using System;
    using TestingContextLimitedInterface.Tokens;

    internal class GroupToken : IToken
    {
        public GroupToken(Type type)
        {
            Type = type;
        }

        public string Name { get; set; }
        public Type Type { get; }

        public override string ToString()
        {
            return Type.Name;
        }
    }
}
