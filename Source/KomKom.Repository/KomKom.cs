using System.Data.Entity;
using KomKom.Model.Breeding;
using KomKom.Model.Farming;
using KomKom.Model.Fishing;
using KomKom.Model;


namespace KomKom.Repository
{
    public class KomKom : DbContext
    {
        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<FarmingProductOffer> FarmingProductOffers { get; set; }
        public DbSet<Fisherman> Fishermen { get; set; }
        public DbSet<FishingProductOffer> FishingProductOffers { get; set; }
        public DbSet<Breeder> Breeders { get; set; }
        public DbSet<BreedingProductOffer> BreedingProductOffers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<MapLocator> MapLocators { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<User> Users { get; set; }

        public KomKom(): base("DefaultConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<KomKom>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // inherited table types
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Farmer>().ToTable("Users_Farmers");
            modelBuilder.Entity<Breeder>().ToTable("Users_Breeders");
            modelBuilder.Entity<Fisherman>().ToTable("Users_Fishermen");
            modelBuilder.Entity<Offer>().ToTable("Offers");
            modelBuilder.Entity<BreedingProductOffer>().ToTable("Offers_BreedingProductOffers");
            modelBuilder.Entity<FarmingProductOffer>().ToTable("Offers_FarmingProductOffers");
            modelBuilder.Entity<FishingProductOffer>().ToTable("Offers_FishingProductOffers");
        }
    }
}
