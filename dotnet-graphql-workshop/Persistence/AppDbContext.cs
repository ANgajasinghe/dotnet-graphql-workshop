
using dotnet_graphql_workshop.Domain;
using Microsoft.EntityFrameworkCore;


namespace dotnet_graphql_workshop.Persistence;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
             : base(options)
        {
        }

        public DbSet<Speaker>? Speakers { get; set; }
    }

