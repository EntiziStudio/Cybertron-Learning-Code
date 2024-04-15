using Allspark.Application.DependencyInjection;
using Allspark.Infrastructure.Data;
using Allspark.Infrastructure.DependencyInjection;
using Allspark.Web.Configuration.SwaggerFilters;
using Allspark.Web.Controllers.Auth.SignIn;
using Allspark.Web.Extensions;
using Allspark.Web.Middlewares;

namespace Allspark.Web;

[ExcludeFromCodeCoverage]
public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add the AllsparkDbContext to the DI container
        services.AddDbContext<AllsparkDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("CybertronAllsparkDbConnection"),
                b => b.MigrationsAssembly("Allspark.Infrastructure")
                    .EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null)));

        services.AddAllsparkApplicationDI();
        services.AddAllsparkInfrastructureDI();
        services.AddAuthService();
        services.AddAllsparkCors();
        services.AddAllsparkOptions(Configuration);
        services.AddAllsparkJWT(Configuration);
        services.AddAutoMapper(typeof(Startup).Assembly);
        services.AddAutoMapper(typeof(AllsparkApplicationDI).Assembly);
        services.AddAutoMapper(typeof(AllsparkInfrastructureDI).Assembly);
        services.AddHealthChecks().AddDbContextCheck<AllsparkDbContext>();
        services.AddHttpContextAccessor();

        // Add Swagger to the DI container
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Allspark API", Version = "v1" });
            c.DocumentFilter<HealthCheckEndpointDocumentFilter>();

            c.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });

        });

        // Add controllers to the DI container
        services.AddControllers();

        // Add validators to the DI container
        services.AddTransient<IValidator<SignInRequest>, SignInValidator>();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Use the exception handling middleware
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        if (env.IsDevelopment())
        {
            // Use Swagger UI in development environment
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Allspark API V1");
                c.RoutePrefix = string.Empty;
            });
        }
        else
        {
            // Use global exception handling middleware in production environment
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "text/html";

                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (errorFeature != null)
                    {
                        var errorMessage = errorFeature.Error.Message;
                        await context.Response.WriteAsync($"<h1>Error: {errorMessage}</h1>").ConfigureAwait(false);
                    }
                });
            });

            app.UseHsts();
        }

        //HealthCheck Database
        app.UseHealthChecks("/api/health", new HealthCheckOptions
        {

            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";
                var response = new HealthCheckResponse
                {
                    StatusCode = report.Status,
                    HealthChecks = report.Entries.Select(x => new IndividualHealthCheckResponse
                    {
                        Component = x.Key,
                        StatusCode = x.Value.Status,
                        Description = x.Value.Description ?? string.Empty,
                    }),
                    HealthCheckDuration = report.TotalDuration
                };
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        });

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(SecurityExtension.DefaultPolicy);
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
