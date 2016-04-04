using System;
using Toolbox.Errors;

namespace Toolbox.WebApi.Exceptions
{
    public class ExceptionLogMessage
    {
        public int HttpStatusCode { get; set; }
        public Error Error { get; set; }
        public Exception Exception { get; set; }
    }
}
