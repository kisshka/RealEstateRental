using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Real_Estate_Rental_Practic.Areas.Identity.Data;

namespace Real_Estate_Rental_Practic.Data;

public class Real_Estate_Rental_PracticIdentityContext : IdentityDbContext<ApplicationUser>
{
    public Real_Estate_Rental_PracticIdentityContext(DbContextOptions<Real_Estate_Rental_PracticIdentityContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityUserLogin<string>>()
            .HasKey(l => l.UserId);

        builder.Entity<IdentityUserLogin<string>>()
            .HasAlternateKey(l => new { l.LoginProvider, l.ProviderKey });
    }
}
