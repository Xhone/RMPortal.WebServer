using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMPortal.WebServer.ExtendModels;
using RMPortal.WebServer.Helpers;
using RMPortal.WebServer.Models.SGS;

namespace RMPortal.WebServer.Data
{
    public class SGSContext:DbContext
    {
        public virtual DbSet<MaGlobalDet> MaGlobalDets { get; set; }
        public virtual DbSet<MaType> MaTypes { get; set; }
        public virtual DbSet<GenPOData> GenPODatas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //var sqlConnectionStringBuilder = new SqlConnectionStringBuilder
            //{

            //};
            ConnectionStrings connectionStrings = AppSettingsHelper.ReadObject<ConnectionStrings>("ConnectionStrings");
            connectionStrings.SGS += "Password=e119769";
            optionsBuilder.UseSqlServer(connectionStrings.SGS);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MaGlobalDet>(eb =>
            {
                eb.HasNoKey();
            });
            modelBuilder.Entity<MaType>(eb =>
            {
                eb.HasNoKey();
            });
            modelBuilder.Entity<GenPOData>(eb =>
            {
                eb.HasNoKey();
            });
            //base.OnModelCreating(modelBuilder);
        }


    }
}
