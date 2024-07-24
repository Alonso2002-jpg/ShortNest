using Context.Models;
using Microsoft.EntityFrameworkCore;

namespace Context.Context;

public class AppContext : DbContext
{
    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
        
    }
    
    public DbSet<ScriptLog> ScriptLogs { get; set; }
    public DbSet<UrlStorage> UrlStorages { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
        
            entity.SetTableName(entity.GetTableName().ToLower());

        
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.GetColumnName().ToLower());
            }
        }

        modelBuilder.Entity<User>()
            .HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId);

        base.OnModelCreating(modelBuilder);
    }
}