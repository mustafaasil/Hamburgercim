using Hamburgercim.Models.Data.Classes;
using Microsoft.EntityFrameworkCore;

namespace Hamburgercim.Models.Data.Context
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options)
        {

        }

        public DbSet<Hamburger> Hamburgerler => Set<Hamburger>();
        public DbSet<Icecek> Icecekler => Set<Icecek>();
        public DbSet<EkstraMalzeme> EkstraMalzemeler => Set<EkstraMalzeme>();
        public DbSet<Siparis> Siparisler => Set<Siparis>();
        public DbSet<Boyut> Boyutlar => Set<Boyut>();


    }
}
