using FinalTestProject.Data;
using FinalTestProject.Models;
using FinalTestProject.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FinalTestProject.Components.Pages
{
    public partial class OgrenciDersSecimi
    {
        [Inject] private SessionState SessionState { get; set; }
        [Inject] private UbysSystemDbContext _context { get; set; }

        private DersSecimi DersSecimi { get; set; }
        private Ogrenci Ogrenci { get; set; }

        private List<Ders>? DersList { get; set; } = [];
        private List<Ders>? AlttanDersList { get; set; } = new List<Ders>();

        private List<SecilenDers>? SecilenDersList { get; set; } = new List<SecilenDers>();

        private string ButtonText = "Add ";

        private bool IsAKTSExceeded = false;
        private bool IsSecimSent = false;

        private string errorMessage = "";

        protected override async Task OnInitializedAsync()
        {
            if (!SessionState.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/", true);
            }

            _context ??= await UbysSystemDbContext.CreateDbContextAsync();

            await GetDersList();
            DersSecimi = new DersSecimi(); 
            DersSecimi.SecilenDersler = new List<string>(); 

        }

        private async Task GetDersList()
        {
            if (_context is not null)
            {
                await GetOgrenci();
                await OgrControl(); //ogrenci bilgilerinin kontrolu - alttan ders var mý vs.
                DersList = await _context.Ders.Where(d => d.Yariyil == Ogrenci.Yariyil && d.OgretimElemaniTc != null).ToListAsync();
            }
        }

        private async Task GetOgrenci()
        {
            if (_context is not null)
            {
                Ogrenci = SessionState.AssignedHesap as Ogrenci;
            }
        }

        public void OnClickAddButton(Ders ders)
        {
            DersSecimi ??= new()
                {
                    SecilenDersler = [],
                    DersList = []
                };

            if (GetToplamAKTS() + ders.AKTS > ValidValues.AKTSLimit)
            {
                IsAKTSExceeded = true;
                errorMessage = "Mevcut AKTS'den (45) fazla AKTS'ye sahip ders secimi yapamazsiniz!";
                return;
            }
            else
            {
                IsAKTSExceeded = false;
            }
            ders.IsDersAdded = true;
            DersSecimi.OgrenciKimlikNo = SessionState.AssignedHesap.TCKimlikNo;
            DersSecimi.DanismanKimlikNo = (SessionState.AssignedHesap as Ogrenci).OgrenciDanismani.ToString();
            DersSecimi.SecilenDersler.Add(ders.DersKodu);
            DersSecimi.DersList.Add(ders);
            DersList.Remove(ders);
        }

        private float GetToplamAKTS()
        {
            float toplamAKTS = 0;
            DersSecimi.DersList.ForEach(d => 
            {
                toplamAKTS += d.AKTS;
            });
            return toplamAKTS;
        }

        private async Task AddDersSecimi()
        {
            if (_context is not null)
            {
                _context.DersSecimi.Add(DersSecimi);

                await AddSecilenDersList();

                IsSecimSent = true;
                errorMessage = "Ders secimi basirili bir sekilde danismana gonderildi!";
            }
            await _context.SaveChangesAsync();
            DersSecimi = null;
        }

        private async Task AddSecilenDersList()
        {
            if (DersSecimi.SecilenDersler != null)
            {
                foreach (var d in DersSecimi.SecilenDersler)
                {
                    bool contains = AlttanDersList.Any(ders => ders.DersKodu == d.Trim());
                    if (contains)
                    {
                        var cikarilacak = AlttanDersList.FirstOrDefault(ders => ders.DersKodu == d.Trim());
                        AlttanDersList.Remove(cikarilacak);
                    }

                    var secilen_ders = new SecilenDers()
                    {
                        TcKimlikNo = DersSecimi.OgrenciKimlikNo,
                        DersKodu = d.Trim(),
                    };

                    SecilenDersList.Add(secilen_ders);
                }

                _context.SecilenDers.AddRange(SecilenDersList);
            }
            else
            {
                errorMessage = "Ders kaydinizi yapmak icin en az bir ders secmelisiniz!";
            }

        }

        private void RemoveDersFromSecim(Ders ders) 
        {
            DersList.Add(ders);
            DersSecimi.DersList.Remove(ders);
            DersSecimi.SecilenDersler.Remove(ders.DersKodu);
            ders.IsDersAdded = false;
        }

        private async Task OgrControl()
        {
            var tum_dn = await _context.DersNotu.Where(dn => dn.OgrenciTc == Ogrenci.TCKimlikNo).ToListAsync();
            var ders_notlari = await _context.DersNotu.Where(dn=>dn.OgrenciTc == Ogrenci.TCKimlikNo && dn.Ders.Yariyil == Ogrenci.Yariyil).ToListAsync();
            var alttan_ders_notlari = new List<DersNotu>();

            if (gnoHesapla(tum_dn) < 1.80)
            {
                alttan_ders_notlari.AddRange(tum_dn);
            }
            else 
            {
                foreach (var d_not in ders_notlari)
                {
                    if (gnoHesapla(tum_dn) >= 1.80 && (d_not.YoklamaDurumu == 0 || d_not.SonucNotu < 50))
                    {
                        alttan_ders_notlari.Add(d_not);
                    }
                }
            }

            if(alttan_ders_notlari != null) 
            { 
                GetAlttanDersler(alttan_ders_notlari);
                try
                {
                    if (AlttanDersList != null && AlttanDersList.Count() > 0)
                    {
                        errorMessage = "Secim yaparsaniz su dersler seciminize eklenecek";
                        foreach (var ad in AlttanDersList)
                        {
                            errorMessage += $"Ders Kodu: {ad.DersKodu} {ad.DersAdi} AKTS: {ad.AKTS}  ";
                            SecilenDersList.Add(new SecilenDers()
                            {
                                TcKimlikNo = Ogrenci.TCKimlikNo,
                                DersKodu = ad.DersKodu.Trim()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = "Ders Seciminiz gerceklestirilemedi :(";
                }


            }
        }

        private double gnoHesapla(List<DersNotu> dn)
        {
            double toplam_kredi = 0.0;
            double agirlik_toplami = 0.0;

            foreach (var d_not in dn)
            {
                if (d_not.Ders != null)
                {
                    toplam_kredi += d_not.Ders.Kredi;
                    agirlik_toplami += d_not.Ders.Kredi * d_not.SonucNotu;
                }
            }

            return agirlik_toplami/ toplam_kredi;
        }

        private void GetAlttanDersler(List<DersNotu> dn)
        {
            foreach (var d_not in dn) 
            {
                if (d_not.Ders != null)
                {
                    AlttanDersList.Add(d_not.Ders);
                }
            }
        }
    }
}