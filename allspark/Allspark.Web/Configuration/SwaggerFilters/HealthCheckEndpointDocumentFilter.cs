namespace Allspark.Web.Configuration.SwaggerFilters;

public class HealthCheckEndpointDocumentFilter : IDocumentFilter
{
    public const string HealthCheckEndpoint = "/api/health";

    public void Apply(OpenApiDocument openApiDocument, DocumentFilterContext context)
    {
        var schema = context.SchemaGenerator.GenerateSchema(typeof(HealthCheckResponse), context.SchemaRepository);

        var healthyResponse = new OpenApiResponse();
        healthyResponse.Content.Add("application/json", new OpenApiMediaType() { Schema = schema });
        healthyResponse.Description = "API service is healthy";

        var unhealthyResponse = new OpenApiResponse();
        unhealthyResponse.Content.Add("application/json", new OpenApiMediaType() { Schema = schema });
        unhealthyResponse.Description = "API service is not healthy";

        var operation = new OpenApiOperation();
        operation.Description = "Returns the health status of this service";
        operation.Tags.Add(new OpenApiTag { Name = "Health Check API" });
        operation.Responses.Add(HttpStatusCode.OK.ToString(), healthyResponse);
        operation.Responses.Add(HttpStatusCode.ServiceUnavailable.ToString(), unhealthyResponse);

        var pathItem = new OpenApiPathItem();
        pathItem.AddOperation(OperationType.Get, operation);

        openApiDocument.Paths.Add(HealthCheckEndpoint, pathItem);
    }
}