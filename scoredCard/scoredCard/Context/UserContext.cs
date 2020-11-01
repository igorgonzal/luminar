
using Microsoft.EntityFrameworkCore;
using scoredCard.Models;

namespace scoredCard.Context
{
    public class UserContext : DbContext
    {

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
        }
    }
}
