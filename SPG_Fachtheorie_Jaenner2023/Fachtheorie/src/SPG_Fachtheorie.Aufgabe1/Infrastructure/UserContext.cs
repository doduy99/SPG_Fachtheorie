using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe1.Infrastructure
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<UserStandard> Standards => Set<UserStandard>();
        public DbSet<UserPremium> Premiums => Set<UserPremium>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Podcast> Podcasts => Set<Podcast>();
        public DbSet<RadioStation> RadioStations => Set<RadioStation>();
        public DbSet<Rating> Ratings => Set<Rating>();
        public DbSet<Favorite> Favoriten => Set<Favorite>();
        public UserContext(DbContextOptions options)
            : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>().HasDiscriminator(u => u.UserType);
        }
    }
}
