using Example.WebApp.Infrastructure;
using Example.WebApplication.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddTransient<IEmailSender, SendGridEmailSender>();

builder.Services.AddDbContext<SiteContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("(default)");
    options.UseSqlite(connectionString);
});

// Auth.
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<SiteContext>();

builder.Services.AddAuthentication().AddFacebook(facebookOptions =>
{
    facebookOptions.AppId = builder.Configuration["Authentication:Facebook:AppId"];
    facebookOptions.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
});

// https://blog.datalust.co/using-serilog-in-net-6/
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day));

builder.Services.AddSingleton<ICountryReader, CountryReader>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Auth: servono sia UseAuthentication, sia UseAuthorization.
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

await InitContext();

app.Run();

async Task InitContext()
{
    using var scope = app.Services.CreateScope();
    using var context = scope.ServiceProvider.GetRequiredService<SiteContext>();

    await context.Database.EnsureCreatedAsync();
}
