using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Data;
using RMPortal.WebServer.Models.Mpo;
using RMPortal.WebServer.Models.Sys;

namespace RMPortal.WebServer.Data
{
    public class RMPortalContext:DbContext
    {
        public RMPortalContext(DbContextOptions<RMPortalContext> options) : base(options) 
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TxMpoHd> TxMpoHds { get; set; }
        public DbSet<TxMpoDet>  TxMpoDets { get; set; }
        public DbSet<TxMpoDetMr> TxMpoDetMrs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Menu>().ToTable("Menus");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<TxMpoHd>().ToTable("TxMpoHd");

            modelBuilder.Entity<TxMpoMatDet>().ToTable("TxMpoMatDet")
                .HasOne(m=>m.TxMpoHd)
                .WithMany(h=>h.TxMpoMatDets) 
                .HasForeignKey(m=>m.MpoNo)
                .HasPrincipalKey(h=>h.MpoNo)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<TxMpoDet>().ToTable("TxMpoDet")
                .HasOne(d => d.TxMpoHd)
                .WithMany(h => h.TxMpoDets)
                .HasForeignKey(d => d.MpoNo)
                .HasPrincipalKey(h => h.MpoNo)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<TxMpoDetMr>().ToTable("TxMpoDetMr")
                .HasOne(m => m.TxMpoDet)
                .WithMany(d => d.TxMpoDetMrs)
                .HasForeignKey("MpoDetId","MpoNo")
                .HasPrincipalKey("MpoDetId","MpoNo")
                .OnDelete(DeleteBehavior.ClientCascade);


            modelBuilder.Entity<TxMpoSurcharge>().ToTable("TxMpoSurcharge")
                .HasOne(s => s.TxMpoHd)
                .WithMany(h => h.TxMpoSurcharges)
                .HasForeignKey(s => s.MpoNo)
                .HasPrincipalKey(h => h.MpoNo)
                .OnDelete(DeleteBehavior.ClientCascade);
                         
                                           
            
        }  
       

    }
}
