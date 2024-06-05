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

        private Danisman? Danisman { get; set; }

        private Ogrenci? SecilenOgr { get; set; }

        private List<DersSecimi> DersSecimleriList { get; set; }

        private List<Ogrenci> TanimliOgrencilerList { get; set; }
        private List<Ders> DersList { get; set; }

        private List<Ders> SecilenDersList { get; set; } //

        private string errorMessage = "";

        protected override async Task OnInitializedAsync()
        {
            _context ??= await UbysSystemDbContext.CreateDbContextAsync();

            await GetDersSecimleri();
            await GetTanimliOgrenciler();
        }

        private async Task GetDersSecimleri()
        {
            if (_context is not null)
            {
                GetDanisman();
                DersSecimleriList = await _context.DersSecimi.Where(d => d.DanismanKimlikNo == Danisman.TCKimlikNo.ToString()).ToListAsync();
                DersList = await _context.Ders.ToListAsync();
            }
        }

        private async Task GetDanisman()
        {
            if (_context is not null)
            {
                Danisman = await _context.Danisman.FirstOrDefaultAsync(d => d.TCKimlikNo.ToString() == SessionState.AssignedHesap.TCKimlikNo.ToString());
            }
        }
        private async Task GetTanimliOgrenciler()
        {
            if (_context is not null)
            {
                foreach (var d in DersSecimleriList)
                {
                    TanimliOgrencilerList = await _context.Ogrenci.Where(o => o.TCKimlikNo == d.OgrenciKimlikNo && o.OgrenciDanismani == SessionState.AssignedHesap.TCKimlikNo).ToListAsync();
                }
            }
        }
        private Ogrenci GetOgrenci(DersSecimi dersSecimi)
        {
            var ogrenci = new Ogrenci();
            
            foreach (var item in TanimliOgrencilerList)
            {
                if (dersSecimi.OgrenciKimlikNo == item.TCKimlikNo) 
                {
                    ogrenci = item;
                    break;
                }
            }

            return ogrenci;
        }

        private List<string> GetDersler(List<string> dersID)
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

        private async Task Onayla(DersSecimi dersSecimi)
        {
            var eklenecek_dnler = new List<DersNotu>();
            SecilenOgr = GetOgrenci(dersSecimi);
            SecilenOgr.SecilmisDersler = dersSecimi.SecilenDersler;

            List<DersNotu> varolan_dnler = await _context.DersNotu.Where(dn => dn.OgrenciTc == SecilenOgr.TCKimlikNo).ToListAsync();

            if (SecilenOgr.SecilmisDersler != null || SecilenOgr.SecilmisDersler.Count() > 0)
            {
                foreach (var ders_kodu in SecilenOgr.SecilmisDersler)
                {
                    eklenecek_dnler.Add(new DersNotu()
                    {
                        OgrenciTc = SecilenOgr.TCKimlikNo,
                        DersKodu = ders_kodu.Trim(),
                    });
                }
            }

            _context.DersSecimi.Remove(dersSecimi);
            _context.DersNotu.RemoveRange(varolan_dnler);
            _context.DersNotu.AddRange(eklenecek_dnler);
            //await AddSecilenDersler();
            _context.Ogrenci.Update(SecilenOgr);
            DersSecimleriList.Remove(dersSecimi);

            await _context.SaveChangesAsync();

            errorMessage = $"{SecilenOgr.Ad} ogrencisinin {SecilenOgr.Yariyil}. yariyili icin ders kaydi basariyla guncellendi!";

        }
        private async Task Reddet(DersSecimi dersSecimi)
        {
            var ogrenci = GetOgrenci(dersSecimi);
            DersSecimleriList.Remove(dersSecimi);
            _context.DersSecimi.Remove(dersSecimi);
            DersSecimleriList.Remove(dersSecimi);

            await _context.SaveChangesAsync();

            errorMessage = $"{ogrenci.Ad} ogrencisinin {ogrenci.Yariyil}. yariyili icin olusturdugu ders kaydi reddedildi!";
        }

        private async Task AddSecilenDersler()
        {
            var eklenecek_dnler = new List<DersNotu>();

            if (SecilenOgr.SecilmisDersler != null || SecilenOgr.SecilmisDersler.Count() > 0)
            {
                foreach (var ders_kodu in SecilenOgr.SecilmisDersler)
                {
                    eklenecek_dnler.Add(new DersNotu()
                    {
                        OgrenciTc = SecilenOgr.TCKimlikNo,
                        DersKodu = ders_kodu.Trim(),
                    });
                }

                _context.DersNotu.AddRange(eklenecek_dnler);
            }
        }
    }
}
