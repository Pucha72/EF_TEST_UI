using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EF_TEST_UI.Models
{
    public class LocalContext : DbContext
    {
        public virtual DbSet<Student> student { get; set; }
        public virtual DbSet<Standard> standard { get; set; }
        public LocalContext() : base("LocalContextDB")
        // public LocalContext() : base("name=SchoolDBConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LocalContext, EF_TEST_UI.Models.DBMigration>());
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}