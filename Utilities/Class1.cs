using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Utilities
{
   public static class MyService
    {
        //this class will serve for my depencency injection for auth0
        
        public static IServiceCollection AddAuth0USer(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie()
            .AddOpenIdConnect("Auth0", options =>
            {
                 options.Authority = $"https://{configuration["Auth0:Domain"]}";
                 options.ClientId = configuration["Auth0:ClientId"];
                 options.ClientSecret = configuration["Auth0:ClientSecret"];
                 options.ResponseType = OpenIdConnectResponseType.Code;
                 options.Scope.Clear();
                 options.Scope.Add("openid");
                 options.Scope.Add("profile");
                 options.Scope.Add("email");

                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     NameClaimType = "name"
                 };

                 options.CallbackPath = new PathString("/callback");
                 options.ClaimsIssuer = "Auth0";

                 options.SaveTokens = true;

                 options.Events = new Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectEvents
                 {
                     OnRedirectToIdentityProviderForSignOut = (context) =>
                     {
                         var logoutUri = $"https://{configuration["Auth0:Domain"]}/v2/logout?client_id={configuration["Auth0:ClientId"]}";
                         var postLogoutUri = context.Properties.RedirectUri;
                         if (!string.IsNullOrEmpty(postLogoutUri))
                         {
                             if (postLogoutUri.StartsWith("/"))
                             {
                                 var request = context.Request;
                                 postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                             }
                             logoutUri += $"&returnTo{Uri.EscapeDataString(postLogoutUri)}";
                         }
                         context.Response.Redirect(logoutUri);
                         context.HandleResponse();
                         return Task.CompletedTask;
                     }
                 };
             });

            return services;
        }
    }
}