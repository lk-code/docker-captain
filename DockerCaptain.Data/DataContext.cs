using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DockerCaptain.Data;

public class DataContext : DbContext
{
    public DbSet<Models.Image> Images { get; set; } = null!;
    public DbSet<Models.Container> Container { get; set; } = null!;

    public static string DatabasePath { get; set; } = string.Empty;

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
        // default for migrations, etc.
        if (string.IsNullOrEmpty(DataContext.DatabasePath))
        {
            string executionLocation = Assembly.GetExecutingAssembly().Location;
            string executionPath = Path.GetDirectoryName(executionLocation)!;
            string dbPath = Path.Combine(executionPath, "migrations.db");

            DataContext.DatabasePath = dbPath;
        }

        try
        {
            this.Database.Migrate();
        }
        catch (Exception err)
        {
            Console.WriteLine("ERROR AT SQLITE MIGRATION:");
            Console.WriteLine(err.Message);
            Console.WriteLine(err.StackTrace);
        }
    }

    public string GetDatabaseFilePath()
    {
        return DataContext.DatabasePath;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(
                $"Filename={DataContext.DatabasePath}",
                options => options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Map table names
        modelBuilder.Entity<Models.Image>().ToTable("Images");
        modelBuilder.Entity<Models.Image>(entity =>
        {
            entity.HasKey(e => e.DockerId);
        });

        modelBuilder.Entity<Models.Container>().ToTable("Container");
        modelBuilder.Entity<Models.Container>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        base.OnModelCreating(modelBuilder);
    }
}