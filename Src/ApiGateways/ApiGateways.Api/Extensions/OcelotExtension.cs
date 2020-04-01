using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateways.Api.Extensions
{
    public static class OcelotExtension
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            var authenticationProviderKey = "OcelotKey";
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            //services.AddAuthentication()
            //    .AddIdentityServerAuthentication(authenticationProviderKey, x =>
            //    {
            //        x.Authority = "http://localhost:54860";
            //        x.Audience = "SampleService";
            //    });

            Action<IdentityServerAuthenticationOptions> opt = o =>
            {
                o.Authority = "http://localhost:54860";
                //o.ApiName = "SampleService";
                //o.SupportedTokens = SupportedTokens.Both;
                //o.RequireHttpsMetadata = false;
            };

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(authenticationProviderKey, opt);
            //services.AddAuthentication()
            //    .AddIdentityServerAuthentication(authenticationProviderKey, options =>
            //    {
            //        options.Authority = "http://localhost:54860/Authentication";
            //        //options.ApiName = "socialnetwork";
            //        options.SupportedTokens = SupportedTokens.Jwt;
            //        //options.ApiSecret = "secret";
            //        options.RequireHttpsMetadata = false;
            //    });
            //    services.AddAuthentication()
            //.AddJwtBearer(authenticationProviderKey, x =>
            //{
            //    x.Authority = "http://localhost:54860";
            //    x.Audience = "test";
            //});

            //Action<IdentityServerAuthenticationOptions> options = o =>
            //{
            //    o.Authority = "http://localhost:54860";
            //    o.ApiName = "api";
            //    o.SupportedTokens = SupportedTokens.Both;
            //    o.ApiSecret = "secret";
            //};

            //services.AddAuthentication()
            //    .AddIdentityServerAuthentication(authenticationProviderKey, options);

            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            //var identityUrl = configuration.GetValue<string>("urls:identity");

            ////var authenticationProviderKey = "OcelotKey";

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            //})
            //.AddJwtBearer(authenticationProviderKey,options =>
            //{
            //    options.Authority = identityUrl;
            //    options.RequireHttpsMetadata = false;
            //    options.Audience = "mobileshoppingagg";
            //});

            return services;
        }
    }
}
