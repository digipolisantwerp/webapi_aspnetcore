using System;

namespace Toolbox.WebApi.Versioning
{
    public interface IVersionProvider
    {
        AppVersion GetCurrentVersion();
    }
}
