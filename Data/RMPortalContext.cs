using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RMPortal.WebServer.Models;
using System.Data;
using RMPortal.WebServer.Models.Mpo;

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
        public DbSet<TxMpoHd> TxMpoHds { get; set; }
        public DbSet<TxMpoDet>  TxMpoDets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Menu>().ToTable("Menus");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<TxMpoHd>().ToTable("TxMpoHd");
            modelBuilder.Entity<TxMpoDet>().ToTable("TxMpoDet");

            //modelBuilder.Entity<TxMpoDet>().Property("TxMpoHdId").HasColumnName("Id");

          
            
        }  
       

    }
}
