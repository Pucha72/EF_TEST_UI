using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;

namespace EF_TEST_UI.Models
{
    internal sealed class DBMigration : DbMigrationsConfiguration<LocalContext>
    {

        public DBMigration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "LocalContext";
            AutomaticMigrationDataLossAllowed = true;
        }
        protected override void Seed(LocalContext context)
        {

        }
    }
}