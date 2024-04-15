using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Allspark.Web.Configuration.SwaggerFilters;

public class IndividualHealthCheckResponse
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
    public string Component { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
