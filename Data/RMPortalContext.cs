using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RMPortal.WebServer.Models;
using System.Data;

namespace RMPortal.WebServer.Data
{
    public class RMPortalContext:DbContext
    {
        public RMPortalContext(DbContextOptions<RMPortalContext> options) : base(options) 
        {
            
        }
        public DbSet<RMPortal.WebServer.Models.User> Users { get; set; }
        public DbSet<RMPortal.WebServer.Models.Menu> Menus { get; set; }
        public DbSet<RMPortal.WebServer.Models.Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Menu>().ToTable("Menus");
            modelBuilder.Entity<Role>().ToTable("Roles");

        }

    }
}
