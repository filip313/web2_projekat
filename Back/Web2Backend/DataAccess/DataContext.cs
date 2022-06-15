using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<Admin> Admini { get; set; }
        public DbSet<Dostavljac> Dostavljaci { get; set; }
        public DbSet<Potrosac> Potrosaci { get; set; }
        public DbSet<Porudzbina> Porudzbine { get; set; }
        public DbSet<Proizvod> Proizvodi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ServiceData.db");
        }
    }
}
