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
            .HasOne(o => o.OgrDanisman)  
            .WithMany()                    
            .HasForeignKey(o => o.OgrenciDanismani); 
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
        modelBuilder.Entity<DersSecimi>()
            .Property(e => e.SecilenDersler)
            .HasConversion(new StringListConverter());
        modelBuilder.Entity<DersSecimi>().HasKey(o => o.OgrenciKimlikNo);

        modelBuilder.Entity<SecilenDers>()
            .HasKey(sd => sd.Id);

        modelBuilder.Entity<SecilenDers>()
            .HasOne<DersSecimi>()
            .WithMany()
            .HasForeignKey(sd => sd.TcKimlikNo)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SecilenDers>()
            .HasOne<Ders>()
            .WithMany()
            .HasForeignKey(sd => sd.DersKodu)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DersNotu>(entity =>
        {
            entity.HasKey(e => e.Id); // Primary Key
            entity.HasOne(e => e.Ogrenci)
                  .WithMany()
                  .HasForeignKey(e => e.OgrenciTc)
                  .OnDelete(DeleteBehavior.Restrict); // Foreign Key to Ogrenci

            entity.HasOne(e => e.Ders)
                  .WithMany()
                  .HasForeignKey(e => e.DersKodu)
                  .OnDelete(DeleteBehavior.Restrict); // Foreign Key to Ders

            
            entity.Property(e => e.SonucNotu).IsRequired();
            entity.Property(e => e.HarfNotu).IsRequired();
            entity.Property(e => e.YoklamaDurumu).IsRequired();

            
            entity.OwnsOne(e => e.AraSinav, sa =>
            {
                sa.Property(p => p.yuzde).HasColumnName("AraSinavYuzde").HasConversion<float>().HasDefaultValue(40.0f);
                sa.Property(p => p.ogr_not).HasColumnName("AraSinavNot");
            });

            entity.OwnsOne(e => e.FinalSinav, sf =>
            {
                sf.Property(p => p.yuzde).HasColumnName("FinalSinavYuzde").HasConversion<float>().HasDefaultValue(60.0f);
                sf.Property(p => p.ogr_not).HasColumnName("FinalSinavNot");
            });
        });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Danisman> Danisman { get; set; }
    public DbSet<Ders> Ders { get; set; }
    public DbSet<Idareci> Idareci { get; set; }
    public DbSet<Ogrenci> Ogrenci { get; set; }
    public DbSet<OgretimElemani> OgretimElemani { get; set; }
    public DbSet<DersNotu> DersNotu { get; set; }
    public DbSet<DersSecimi> DersSecimi { get; set; }

    public DbSet<SecilenDers> SecilenDers { get; set; }

}
