using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DockerCaptain.Data;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        string migrationsDatabaseFile = "migrations.db";
        File.Delete(migrationsDatabaseFile);
        File.Create(migrationsDatabaseFile);

        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlite("Data Source=migrations.db");

        return new DataContext(optionsBuilder.Options);
    }
}
