using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SecLog.Models;

namespace SecLog.Data
{
    public class SecLogContext : DbContext
    {
        public SecLogContext() : base("name=SecLogDb")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<SecLogContext>());
        }

        public DbSet<SecurityEvent> SecurityEvents { get; set; }
    }
}
