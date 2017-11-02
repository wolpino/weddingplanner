using Microsoft.EntityFrameworkCore;
 
namespace weddingplanner.Models
{
    public class WeddingContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public WeddingContext(DbContextOptions<WeddingContext> options) : base(options) { }
        public DbSet<User> users {get;set;}
        public DbSet<Wedding> weddings {get;set;}
        public DbSet<RSVP> rsvp {get;set;}

    }
}
