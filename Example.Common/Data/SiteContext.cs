using Example.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Example.Common.Data;

public class SiteContext : IdentityDbContext<IdentityUser>
{
    public SiteContext(DbContextOptions<SiteContext> options)
        : base(options)
    { }

    public DbSet<UserRegistration> UserRegistrations => Set<UserRegistration>();
}

// Supporto design-time per le migrazioni non essendo la classe SiteContext nei progetti che lo inizializzano.
// https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli.
public class SiteContextFactory : IDesignTimeDbContextFactory<SiteContext>
{
    public SiteContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SiteContext>();
        optionsBuilder.UseSqlite(@"Data Source=..\site.db");

        return new SiteContext(optionsBuilder.Options);
    }
}