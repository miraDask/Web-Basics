namespace BattleCards.Data
{
    using BattleCards.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<UserCard> UsersCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCard>().HasKey(x => new { x.UserId, x.CardId });
            modelBuilder.Entity<Card>().HasMany(x => x.UsersCards).WithOne(x => x.Card).HasForeignKey(x => x.CardId);
            modelBuilder.Entity<User>().HasMany(x => x.UsersCards).WithOne(x => x.User).HasForeignKey(x => x.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
