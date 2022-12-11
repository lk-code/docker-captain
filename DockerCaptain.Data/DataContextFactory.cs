﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DockerCaptain.Data;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        Console.WriteLine($"CreateDbContext");

        string migrationsDatabaseFile = "migrations.db";
        File.Delete(migrationsDatabaseFile);
        File.Create(migrationsDatabaseFile);

        Console.WriteLine($"UseSqlite");

        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlite("Data Source=migrations.db");

        Console.WriteLine($"new DataContext");

        return new DataContext(optionsBuilder.Options);
    }
}
