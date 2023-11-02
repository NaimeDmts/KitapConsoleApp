using EvKitapci.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvKitapci.Contexts
{
    public class AppDbContext :DbContext
    {
        public DbSet<Kullanici> Kullanicis { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Yazar> Yazars { get; set; }
        public DbSet<Kitap> Kitaps { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer("Server=DESKTOP-EI4I1FN\\MSSQLSERVER2019;User Id=sa;Password=1702;Database=KitapciDB;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kullanici>().HasIndex(x=>x.Email).IsUnique();
        }
    }
}
