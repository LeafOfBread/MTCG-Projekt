using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Microsoft.EntityFrameworkCore;


namespace SWE.Models
{
    class Database
    {
        static void Main(string[] args)
        {
            // Connection string
            var connectionString = "Host=localhost;Port=5432;Database=YourDatabaseName;Username=YourUsername;Password=YourPassword";

            // Connect to PostgreSQL
            using (var connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection successful!");

                    // Execute queries or commands here
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Connection failed: {ex.Message}");
                }
            }
        }
    }

public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Add DbSet properties for your tables
        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
    }

}
