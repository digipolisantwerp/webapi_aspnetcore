using System;

namespace Toolbox.WebApi.Formatters
{
    public class BadRootObjectException : RootObjectException
    {
        public BadRootObjectException(string message, string typeName) : base(message, typeName)
        { }
    }
}