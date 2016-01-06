using System;

namespace Toolbox.WebApi.Formatters
{
    public class RootObjectMissingException : RootObjectException
    {
        public RootObjectMissingException(string message, string typeName) : base(message, typeName)
        { }
    }
}