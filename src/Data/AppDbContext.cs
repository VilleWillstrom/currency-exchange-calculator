using Microsoft.EntityFrameworkCore;
using currency_exchange_calculator.Models;

namespace currency_exchange_calculator.Data
{
    /// <summary>
    /// Represents the application's database context.
    /// It is responsible for interacting with the database and managing the currency conversion records.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class with specified options.
        /// </summary>
        /// <param name="options">The options to configure the database context.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{TEntity}"/> for the currency conversions.
        /// This represents the table in the database for storing currency conversion records.
        /// </summary>
        public DbSet<CurrencyConversion> CurrencyConversions { get; set; }

        /// <summary>
        /// Configures the model for the database when it's being created.
        /// You can define database schema and relationships here if needed.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure the database schema.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define database models here if needed
        }
    }
}
