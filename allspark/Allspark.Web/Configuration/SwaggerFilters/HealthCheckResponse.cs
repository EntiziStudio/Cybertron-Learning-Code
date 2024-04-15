using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Allspark.Web.Configuration.SwaggerFilters
{
public class HealthCheckResponse
{
    public HealthStatus StatusCode { get; set; }
    public string Status
    {
        get
        {
            switch (StatusCode)
            {
                case HealthStatus.Healthy: return "Healthy!";
                case HealthStatus.Degraded: return "Degraded!";
                default: return "Unhealthy!";
            }
        }
    }
    public IEnumerable<IndividualHealthCheckResponse> HealthChecks { get; set; } = new List<IndividualHealthCheckResponse>();
    public TimeSpan HealthCheckDuration { get; set; }
}
}
