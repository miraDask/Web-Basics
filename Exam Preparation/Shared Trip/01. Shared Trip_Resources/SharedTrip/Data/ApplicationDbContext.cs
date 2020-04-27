namespace SharedTrip
{
    using Microsoft.EntityFrameworkCore;
    using SharedTrip.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<UserTrip> UsersTrips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTrip>().HasKey(x => new { x.UserId, x.TripId});
            modelBuilder.Entity<Trip>().HasMany(x => x.UsersTrips).WithOne(x => x.Trip).HasForeignKey(x => x.TripId);
            modelBuilder.Entity<User>().HasMany(x => x.UsersTrips).WithOne(x => x.User).HasForeignKey(x => x.UserId);
        }
    }
}
