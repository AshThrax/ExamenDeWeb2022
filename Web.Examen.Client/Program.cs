using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Web.Examen.Client.Service;
using Web.Examen.Client.Client;


var builder = WebApplication.CreateBuilder(args);
var configuration= builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();
//----------------------------------------------------------Session
builder.Services.AddSession(options => 
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.IsEssential= true;
    options.Cookie.HttpOnly = true;
});

//------------------------------------------------------------Auth0uthentication-Section 
builder.Services.AddAuthentication( options => 
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie()
.AddOpenIdConnect("Auth0",options =>
    {
        //set the Authority to your Auth0 Domain
        options.Authority = $"https://{configuration["Auth0:Domain"]}";
        //Configure the auth0 client ID And Client Secret
        options.ClientId = configuration["Auth0:ClientId"];
        options.ClientSecret = configuration["Auth0:ClientSecret"];
        //set Response type to code
        options.ResponseType = OpenIdConnectResponseType.Code;
        //Configure the Scope
        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");  
        options.Scope.Add("email");
        //set up permission
        options.Scope.Add("read:messages");
        options.Scope.Add("write:messages");
        options.Scope.Add("read:users");
        options.Scope.Add("write:users");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "name",
            RoleClaimType ="http://schemas.microsoft.com/ws/2008/06/identity/claims/role"

        };
        //callback path String
        options.CallbackPath = new PathString("/callback");
        options.ClaimsIssuer = "Auth0";
       
        options.SaveTokens = true;

        options.Events = new OpenIdConnectEvents
        {
            OnRedirectToIdentityProviderForSignOut = (context) =>
            {

                var logoutUri = $"https://{configuration["Auth0:Domain"]}/v2/logout?client_id={configuration["Auth0:ClientId"]}";

                var postLogoutUri = context.Properties.RedirectUri;
                if (!string.IsNullOrEmpty(postLogoutUri))
                {
                     if(postLogoutUri.StartsWith("/"))
                    { 
                        var request =context.Request;
                        postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                    }
                    logoutUri += $"&returnTo={Uri.EscapeDataString(postLogoutUri)}";
                }
                context.Response.Redirect(logoutUri);
                context.HandleResponse();
                return Task.CompletedTask;
            },  
            OnRedirectToIdentityProvider= context =>
            {
             context.ProtocolMessage.SetParameter("Audience", configuration["Auth0:Audience"]);
             return Task.FromResult(0);
            },
             OnMessageReceived = context =>
             {
                 if (context.ProtocolMessage.Error=="acces_denied")
                 {
                     context.HandleResponse();
                     context.Response.Redirect("/Account/AccessDenied");
                 }
                 return Task.FromResult(0);
             }
        };
        
       
    });
//------------------------------------------------------------retry policies variable

//j'appell mon objet retrypolicies qui 
var retryPolicy = RetryPolicies.GetRetryPolicy();
var circuitBreaker = RetryPolicies.GetRetryPolicy();
//-------------------------------------------------------------httpclient-Section
//Acteur Client
builder.Services.AddHttpClient<IActeurClient, ActeurClient>()
    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
    .AddPolicyHandler(retryPolicy)
    .AddPolicyHandler(circuitBreaker);

//FilmClient
builder.Services.AddHttpClient<IFilmClient,Filmclient>()
    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
    .AddPolicyHandler(retryPolicy)
    .AddPolicyHandler(circuitBreaker); ;

//omdClient
builder.Services.AddHttpClient <IOmdbClient, OmdbClient>()
    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
    .AddPolicyHandler(retryPolicy)
    .AddPolicyHandler(circuitBreaker);

//PostClient
builder.Services.AddHttpClient<IPostClient,PostClient>()
    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
    .AddPolicyHandler(retryPolicy)
    .AddPolicyHandler(circuitBreaker); 

//repostClient
builder.Services.AddHttpClient<IRepostClient,RePostClient>()
    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
    .AddPolicyHandler(retryPolicy)
    .AddPolicyHandler(circuitBreaker); ;

//building app here do not disturb
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
