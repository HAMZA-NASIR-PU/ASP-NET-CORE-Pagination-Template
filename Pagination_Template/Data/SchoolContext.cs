using Microsoft.EntityFrameworkCore;
using Pagination_Template.Models;

namespace Pagination_Template.Data
{
	public class SchoolContext : DbContext
	{
		public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

		public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensure the table name is properly configured
            modelBuilder.Entity<Event>().ToTable("event");
        }
    }
}
