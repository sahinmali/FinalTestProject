using FinalTestProject.Models;
using FinalTestProject.Models.Accounts;
using Microsoft.EntityFrameworkCore;

public class DatabaseHandler
{
    private readonly UbysSystemDbContext _context;

    public DatabaseHandler(UbysSystemDbContext context)
    {
        _context = context;
    }

    //Danisman
    public async Task<List<Danisman>> GetDanismanListAsync()
    {
        return await _context.Danisman.ToListAsync();
    }

    public async Task UpdateDanismanAsync(Danisman danisman)
    {
        _context.Danisman.Update(danisman);
        await _context.SaveChangesAsync();
    }

    //Ogrenci
    public async Task<List<Ogrenci>> GetOgrenciListAsync()
    {
        return await _context.Ogrenci.ToListAsync();
    }

    public async Task UpdateOgrenciAsync(Ogrenci ogrenci)
    {
		_context.Ogrenci.Update(ogrenci);
        await _context.SaveChangesAsync();
    }

    //Idareci, Only Idareci Getter
    public async Task<List<Idareci>> GetIdareciListAsync()
    {
        return await _context.Idareci.ToListAsync();
    }

    //Ogretim Elemani
    public async Task<List<OgretimElemani>> GetOgretimElemaniListAsync()
    {
        return await _context.OgretimElemani.ToListAsync();
    }

    public async Task UpdateOgretimElemaniAsync(OgretimElemani OgretimElemani)
    {
        _context.OgretimElemani.Update(OgretimElemani);
        await _context.SaveChangesAsync();
    }

    //Ders
    public async Task<List<Ders>> GetDersListAsync()
    {
        return await _context.Ders.ToListAsync();
    }

    public async Task UpdateDersAsync(Ders ders)
    {
        _context.Ders.Update(ders);
        await _context.SaveChangesAsync();
    }

}