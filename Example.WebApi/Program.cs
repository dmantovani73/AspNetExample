using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
if (builder.Environment.IsDevelopment()) builder.Services.AddSwaggerDoc();

builder.Services.AddDbContext<SiteContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("(default)");
    options.UseSqlite(connectionString);
});

// Configurazione per accettare le chiamate HTTP cross-origin da WebApp.
var corsPolicyName = "WebApp";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, policy =>
    {
        policy
            .WithOrigins("https://localhost:5001", "http://localhost:5000")
            // Altrimenti non viene accettato il bearer token.
            .AllowAnyHeader();
    });
});

builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<SiteContext>();

builder.Services.AddAuthenticationJWTBearer(builder.Configuration["TokenSigningKey"]);

var app = builder.Build();

// Attenzione: va messo all'inizio della pipeline.
app.UseCors(corsPolicyName);

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints(configure =>
{
    //configure.RoutingOptions = o => o.Prefix = "api";
});

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3(s => s.ConfigureDefaults());
}

app.Run();
