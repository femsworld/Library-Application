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
        public DbSet<Loan> Loans { get; set; }     
        public DbSet<LoanBook> LoanBooks { get; set; }     

        public DatabaseContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _configuration = config;
        }

        static DatabaseContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // var builder = new NpgsqlDataSourceBuilder(_configuration.GetConnectionString("DefaultConnection"));
            //  builder.MapEnum<Role>();
            //  builder.MapEnum<Genre>();
            // optionsBuilder.AddInterceptors(new TimeStampInterceptor());
            // // optionsBuilder.UseNpgsql(builder.Build()).UseSnakeCaseNamingConvention();
            // optionsBuilder.UseNpgsql(builder.Build());
        }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)    
        {
            modelBuilder.HasPostgresEnum<Role>();
            modelBuilder.HasPostgresEnum<Genre>();

            modelBuilder.Entity<User>().
            HasIndex(u => u.Email).IsUnique();
        }
    }
}   