using FinalTestProject.Data;
using FinalTestProject.Models;
using Microsoft.EntityFrameworkCore;

public class UbysSystemDbContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public UbysSystemDbContext(IConfiguration configuration) => Configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(Configuration.GetConnectionString("UbysSystemDB"));
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Warning)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging(true);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ogrenci>()
            .Property(e => e.SecilmisDersler)
            .HasConversion(new StringListConverter());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Danisman> Danisman { get; set; }
    public DbSet<Ders> Ders { get; set; }
    public DbSet<Idareci> Idareci { get; set; }
    public DbSet<Ogrenci> Ogrenci { get; set; }
    public DbSet<OgretimElemani> OgretimElemani { get; set; }

}
