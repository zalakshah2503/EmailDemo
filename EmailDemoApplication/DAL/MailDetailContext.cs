using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EmailDemoApplication.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EmailDemoApplication.DAL
{
    public class MailDetailContext : DbContext
    {
        public MailDetailContext() : base("MailDetailContext")
        {
        }
        public DbSet<MailConfig> MailConfigs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}