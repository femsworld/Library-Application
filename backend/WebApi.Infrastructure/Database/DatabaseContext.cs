using Microsoft.EntityFrameworkCore;
using Npgsql;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Address> Addresses { get; set; }     

        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // var builder = new NpgsqlDataSourceBuilder("Host=localhost; Server=localhost; Port=5432; Database=Library; Username=admin;Password=2003");
            var builder = new NpgsqlDataSourceBuilder(_configuration.GetConnectionString("DefaultConnection"));
            // var builder = new NpgsqlDataSourceBuilder(_configuration.GetConnectionString("DefaultConnection"));
            // builder.MapEnum<Role>();
            // optionsBuilder.AddInterceptors(new TimeStampInterceptor());
            // optionsBuilder.UseNpgsql(builder.Build()).UseSnakeCaseNamingConvention();
            optionsBuilder.UseNpgsql(builder.Build());
        }  
    }
}   