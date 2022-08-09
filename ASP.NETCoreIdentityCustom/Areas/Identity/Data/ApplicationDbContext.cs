using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ASP.NETCoreIdentityCustom.Models;
using ASP.NETCoreIdentityCustom.Entity;

namespace ASP.NETCoreIdentityCustom.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
   public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<DocumentEntity> Documents { get; set; }
    public DbSet<Role> RoleRequests { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        builder.Entity<ApplicationUser>().ToTable("Users"); //AspNetUsers
        builder.Entity<IdentityRole>().ToTable("Roles"); //AspNetRoles
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRole"); //AspNetUserRole
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim"); //AspNetUserClaim
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin"); //AspNetUserLogin
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim"); //AspNetRoleClaim
        builder.Entity<IdentityUserToken<string>>().ToTable("UserToken"); //AspNetUserToken


    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
    }
}
