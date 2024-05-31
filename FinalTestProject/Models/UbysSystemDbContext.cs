
using FinalTestProject.Models;
using FinalTestProject.Models.Accounts;
using Microsoft.EntityFrameworkCore;


public class UbysSystemDbContext : DbContext
{
    public UbysSystemDbContext(DbContextOptions<UbysSystemDbContext> options) : base(options) { }

    public DbSet<Danisman> Danisman { get; set; }
    public DbSet<Ders> Ders { get; set; }
    public DbSet<Idareci> Idareci { get; set; }
    public DbSet<Ogrenci> Ogrenci { get; set; }
    public DbSet<OgretimElemani> OgretimElemani { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Danisman>().HasKey(danisman => danisman.TCKimlikNo);
        modelBuilder.Entity<Idareci>().HasKey(idareci => idareci.TCKimlikNo);
        modelBuilder.Entity<OgretimElemani>().HasKey(oe => oe.TCKimlikNo);
        modelBuilder.Entity<Ogrenci>().HasKey(ogrenci => ogrenci.TCKimlikNo);
        modelBuilder.Entity<Ders>().HasKey(ders => ders.DersKodu);
        base.OnModelCreating(modelBuilder);
    }
}
