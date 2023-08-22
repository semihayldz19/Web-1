using Course.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Data
{
    public partial class DataContext : DbContext
    {
     
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<Companys> companys { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = _options.Value.Connection;
            //var serverVersion = new MySqlServerVersion(new Version(8,0,33));
            //optionsBuilder.UseMySql(connectionString, serverVersion);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        //    OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
