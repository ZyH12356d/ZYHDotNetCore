using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZYHDotNetCore.ConsoleApp
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=DotNetTraining;User ID=sa;Password=sasa@123;TrustServerCertificate=true;");
            }
        }
        public DbSet<Model.BlogModel> Blogs { get; set; }
    }
}
