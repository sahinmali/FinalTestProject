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
            .HasOne(o => o.OgrDanisman)  // Ogrenci s�n�f�n�n Danisman navigasyon �zelli�ine ba�lan�r
            .WithMany()                   // Danisman s�n�f�n�n herhangi bir �zelli�iyle e�le�tirilir (Birden �ok ��renci bir dan��mana sahip olabilir)
            .HasForeignKey(o => o.OgrenciDanismani); // Ogrenci s�n�f�ndaki OgrenciDanismani foreign key ile e�le�tirilir
        modelBuilder.Entity<Ogrenci>()
            .Property(e => e.SecilmisDersler)
            .HasConversion(new StringListConverter());
        modelBuilder.Entity<OgretimElemani>()
            .Property(e => e.SecilmisDersler)
            .HasConversion(new StringListConverter());
        modelBuilder.Entity<Danisman>()
            .Property(e => e.TanimliOgrenciler)
            .HasConversion(new StringListConverter());
        modelBuilder.Entity<Idareci>()
            .Property(e => e.AcilanDersler)
            .HasConversion(new StringListConverter());
        modelBuilder.Entity<Idareci>()
            .Property(e => e.DersListe)
            .HasConversion(new StringListConverter());
        modelBuilder.Entity<Idareci>()
            .Property(e => e.OgretimElemaniList)
            .HasConversion(new StringListConverter());
        modelBuilder.Entity<Ders>()
            .HasOne(o => o.OgretimElemani)
            .WithMany()
            .HasForeignKey(o => o.OgretimElemaniTc);
        modelBuilder.Entity<Ders>()
        .Property(e => e.OgrenciList)
        .HasConversion(new StringListConverter());
        modelBuilder.Entity<DersNotu>().HasNoKey();
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Danisman> Danisman { get; set; }
    public DbSet<Ders> Ders { get; set; }
    public DbSet<Idareci> Idareci { get; set; }
    public DbSet<Ogrenci> Ogrenci { get; set; }
    public DbSet<OgretimElemani> OgretimElemani { get; set; }

}
