using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreLibrary.Controllers.Resources
{
    public class Policies
    {
        public const string Admin = "Admin";
        //public const string Member = "Member";
        public const string Moderator = "Moderator";
        public static AuthorizationPolicy Policy(string role)
        {
            var policy = new AuthorizationPolicyBuilder();
            policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
            policy.RequireAuthenticatedUser().RequireRole(role);

            return policy.Build();
        }
    }
}
/*
 config.AddPolicy(Policies.Admin, policy =>
                {
                    policy.RequireRole(Policies.Admin);
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                });
                config.AddPolicy(Policies.Moderator, policy =>
                {
                    policy.RequireRole(Policies.Moderator);
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                });
                config.AddPolicy(Policies.Member, policy =>
                {
                    policy.RequireRole(Policies.Member);
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                });*/
