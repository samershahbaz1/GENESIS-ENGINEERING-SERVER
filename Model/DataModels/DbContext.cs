using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using WebApplication1.Model.ViewModels;

namespace WebApplication1.Model.DataModels
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<University> Universities { get; set; }
    }
}
