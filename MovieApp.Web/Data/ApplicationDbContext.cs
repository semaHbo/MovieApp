using Microsoft.EntityFrameworkCore;
using MovieApp.Web.Models;

namespace MovieApp.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Movie> Movies { get; set; }
    }
}

