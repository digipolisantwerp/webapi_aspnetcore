namespace Toolbox.WebApi.CorrelationId
{
    public class CorrelationOptions
    {
        public string IdHeaderKey { get; set; } = CorrelationHeaders.IdHeaderKey;
        public string SourceHeaderKey { get; set; } = CorrelationHeaders.SourceHeaderKey;
    }
}
