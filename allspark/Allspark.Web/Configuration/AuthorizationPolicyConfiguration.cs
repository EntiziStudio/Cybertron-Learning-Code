namespace Allspark.Web.Configuration;

public class AuthorizationPolicyConfiguration
{
    public string AdminPolicy = "AdminPolicy";
    public string LectuterPolicy = "LectuterPolicy";
    public string TutorPolicy = "TutorPolicy";
    public string StudentPolicy = "StudentPolicy";

    public void ConfigurePolicies(AuthorizationOptions options)
    {
        options.AddPolicy(AdminPolicy, policy =>
            policy.RequireClaim("role", "admin"));

        options.AddPolicy(LectuterPolicy, policy =>
            policy.RequireClaim("role", "lectuter"));

        options.AddPolicy(TutorPolicy, policy =>
            policy.RequireClaim("role", "tutor"));

        options.AddPolicy(StudentPolicy, policy =>
            policy.RequireClaim("role", "student"));
    }
}
