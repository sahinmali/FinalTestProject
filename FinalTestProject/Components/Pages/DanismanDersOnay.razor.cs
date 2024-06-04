using FinalTestProject.Data;
using FinalTestProject.Models;
using FinalTestProject.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace FinalTestProject.Components.Pages
{
    public partial class DanismanDersOnay
    {
        [Inject] private SessionState SessionState { get; set; }
        [Inject] private UbysSystemDbContext _context { get; set; }

        private List<DersSecimi> DersSecimi { get; set; }

        private List<Ogrenci> OgrenciList { get; set; }
        private List<Ders> DersList { get; set; }

        private string errorMessage;

        protected override async Task OnInitializedAsync()
        {
            _context ??= await UbysSystemDbContext.CreateDbContextAsync();

            await InitializeDersSecimleri();
            await InitializeOgrenciList();
        }

        private async Task InitializeDersSecimleri()
        {
            if (_context is not null)
            {
                DersSecimi = await _context.DersSecimi.Where(d => d.DanismanKimlikNo == SessionState.AssignedHesap.TCKimlikNo.ToString()).ToListAsync();
                DersList = await _context.Ders.ToListAsync();
            }
        }

        private async Task InitializeOgrenciList()
        {
            if (_context is not null)
            {
                foreach (var d in DersSecimi)
                {
                    OgrenciList = await _context.Ogrenci.Where(o => o.TCKimlikNo == d.OgrenciKimlikNo).ToListAsync();
                }
            }
        }
        private Ogrenci GetOgrenci(DersSecimi dersSecimi)
        {
            var ogrenci = new Ogrenci();
            
            foreach (var item in OgrenciList)
            {
                if (dersSecimi.OgrenciKimlikNo == item.TCKimlikNo) 
                {
                    ogrenci = item;
                    break;
                }
            }

            return ogrenci;
        }

        private List<string> GetDersNames(List<string> dersID)
        {
            List<string> dersNameList = [];

            foreach (var item in DersList)
            {
                for (var i = 0; i < dersID.Count; i++)
                {
                    if (item.DersKodu == dersID[i])
                    {
                        dersNameList.Add(item.DersAdi);
                        break;
                    }
                }
            }
            return dersNameList;
        }

        private async Task OnClickDersOnay(DersSecimi dersSecimi)
        {
            var ogrenci = GetOgrenci(dersSecimi);
            ogrenci.SecilmisDersler = dersSecimi.SecilenDersler;
            _context.DersSecimi.Remove(dersSecimi);
            _context.Ogrenci.Update(ogrenci);
            DersSecimi.Remove(dersSecimi);

            await _context.SaveChangesAsync();

        }
        private async Task OnClickDersRet(DersSecimi dersSecimi)
        {
            DersSecimi.Remove(dersSecimi);
            _context.DersSecimi.Remove(dersSecimi);
            DersSecimi.Remove(dersSecimi);

            await _context.SaveChangesAsync();
        }
    }
}
