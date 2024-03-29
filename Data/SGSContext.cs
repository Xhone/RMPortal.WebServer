﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RMPortal.WebServer.ExtendModels;
using RMPortal.WebServer.Helpers;
using RMPortal.WebServer.Models.SGS;
using RMPortal.WebServer.Models.Sys;

namespace RMPortal.WebServer.Data
{
    public class SGSContext:DbContext
    {
        public virtual DbSet<MaGlobalDet> MaGlobalDets { get; set; }
        public virtual DbSet<MaType> MaTypes { get; set; }
        public virtual DbSet<GenPOData> GenPODatas { get; set; }

        public virtual DbSet<JobOrder> JobOrders { get; set; }

        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Surcharge> Surcharges { get; set; }

        public virtual DbSet<Currency> Currencys { get; set; }
        public virtual DbSet<Shipped> Shippeds { get; set; }
        public virtual DbSet<MaMatHead> MaMatHeads { get; set; } 
        
        public virtual DbSet<MaMatDetail> MaMatDetails { get; set; }
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
            modelBuilder.Entity<JobOrder>(eb =>
            {
                eb.HasNoKey();
            });
            modelBuilder.Entity<Supplier>(eb =>
            {
                eb.HasNoKey();
            });
            modelBuilder.Entity<Surcharge>(eb =>
            {
                eb.HasNoKey();
            });

            modelBuilder.Entity<Currency>(
                eb =>
                {
                    eb.HasNoKey();
                });
            modelBuilder.Entity<Shipped>(
                eb => { eb.HasNoKey(); });

            modelBuilder.Entity<MaMatHead>(
              eb => { eb.HasNoKey(); });

            modelBuilder.Entity<MaMatDetail>(
              eb => { eb.HasNoKey(); });
            //base.OnModelCreating(modelBuilder);
        }


    }
}
