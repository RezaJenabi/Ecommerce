using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace CustomerManagement.Api.Extensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddCustomerManagementAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
             
            var authenticationProviderKey = "CustomerManagement";


            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetValue<string>("Auth:Secret")));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = configuration.GetValue<string>("Auth:Iss"),
                ValidateAudience = true,
                ValidAudience = configuration.GetValue<string>("Auth:Audience"),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = authenticationProviderKey;
            })
            .AddJwtBearer(authenticationProviderKey, x =>
            {
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = tokenValidationParameters;
            });

            return services;
        }
    }
}
