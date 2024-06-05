using FinalTestProject.Services;
using FinalTestProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using FinalTestProject.Models;

namespace FinalTestProject.Components.Pages
{
    public partial class OgretimElemaniNotGir
    {
        [Inject] private UbysSystemDbContext DbContext { get; set; }
        [Inject] private SessionState SessionState { get; set; }

        public OgretimElemani? OgretimEleman { get; set; }
        public List<DersNotu> DersNotlari { get; set; } = new List<DersNotu>();
        public List<DersNotu> SecilenDersinNotlari { get; set; } = new List<DersNotu>();

        public List<Ders> TanimliDersler{ get; set; } = new List<Ders>();

        public bool ShowCreate { get; set; }

        private DersNotu? secilenDersNotu { get; set; }
        private Ders? secilenDers { get; set; }
        private string errorMessage;

        protected override async Task OnInitializedAsync()
        {
            ShowCreate = false;
            await GetOgretimElemani();
        }

        private async Task GetOgretimElemani()
        {
            if (SessionState.AssignedHesap is not null && DbContext is not null)
            {
                try
                {
                    OgretimEleman = await DbContext.OgretimElemani
                        .FirstOrDefaultAsync(oe => oe.TCKimlikNo == SessionState.AssignedHesap.TCKimlikNo);

                    if (OgretimEleman is not null)
                    {
                        // Populate other properties if needed
                        await GetTanimliDersNotlari(); // Metodun �a�r�lmas� async olarak yap�lmal�
                        await GetTanimliDersler();
                    }
                    else
                    {
                        errorMessage = "OgretimElemani not found";
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = $"Error fetching OgretimEleman: {ex.Message}";
                }
            }
            else
            {
                errorMessage = "Invalid session state or DbContext not available";
            }
        }

        private void DersSecildi(ChangeEventArgs e)
        {
            var secilenDersKodu = e.Value?.ToString();

            if (string.IsNullOrEmpty(secilenDersKodu))
            {
                secilenDersNotu = null;
                errorMessage = "L�tfen ge�erli bir ders se�iniz.";
                return;
            }

            SecilenDersinNotlari = DersNotlari.Where(d => d.DersKodu == secilenDersKodu).ToList();

            if (SecilenDersinNotlari == null)
            {
                errorMessage = "SecilenDersinNotlari null!.";
            }
            if (SecilenDersinNotlari.Count() == 0)
            {
                errorMessage = "Se�ilen derse ait ders notu bilgisi bulunamadi.";
            }
        }
        private void DersNotuSecildi(int dn_id)
        {
            var secilenDersNotu�d = dn_id;

            if (secilenDersNotu�d == null)
            {
                errorMessage = "L�tfen ge�erli bir ders se�iniz.";
                return;
            }

            secilenDersNotu = DersNotlari.FirstOrDefault(d => d.Id == dn_id);

            if (secilenDersNotu == null)
            {
                errorMessage = "Se�ilen ders notu bulunamad�.";
            }
        }

        public async Task GetTanimliDersNotlari()
        {
            if (DbContext is not null && DersNotlari != null)
            {
                DersNotlari = await DbContext.DersNotu
                                .Include(dn => dn.Ders) // Ders bilgilerini dahil et
                                .Include(dn => dn.Ogrenci)
                                .Where(dn => dn.Ders.OgretimElemaniTc.ToString() == OgretimEleman.TCKimlikNo.ToString())
                                .ToListAsync();

                foreach (var d in DersNotlari)
                {
                    d.HesaplaGecmeNotu();
                }

                // Hata ay�klama i�in mesaj ekleyelim
                Console.WriteLine($"Ders notlar� y�klendi: {DersNotlari.Count} kay�t bulundu.");
            }
        }

        public async Task GetTanimliDersler()
        {
            if (DbContext is not null && DersNotlari != null)
            {
                TanimliDersler = await DbContext.Ders
                                .Where(dn => dn.OgretimElemaniTc.ToString() == OgretimEleman.TCKimlikNo.ToString())
                                .ToListAsync();
            }
        }

        private async Task Kaydet()
        {
            if (secilenDersNotu == null)
            {
                errorMessage = "G�ncellenecek ders notu se�ilmedi.";
                return;
            }

            secilenDersNotu.AraSinav.yuzde = 0.4f;
            secilenDersNotu.FinalSinav.yuzde = 0.6f;

            float araSinavNotu = secilenDersNotu.AraSinav.ogr_not;
            float finalSinavNotu = secilenDersNotu.FinalSinav.ogr_not;
            int yoklama = secilenDersNotu.YoklamaDurumu;

            if (araSinavNotu >= 0 && araSinavNotu <= 100 && finalSinavNotu >= 0 && finalSinavNotu <= 100)
            {
                if(yoklama == 1 || yoklama == 0)
                {
                    try
                    {
                        secilenDersNotu.HesaplaGecmeNotu();
                        // Ders notunu veritaban�nda g�ncelle
                        DbContext.DersNotu.Update(secilenDersNotu);
                        await DbContext.SaveChangesAsync();
                        errorMessage = $"{secilenDersNotu.Ogrenci.Ad}'nin Ders notu ba�ar�yla g�ncellendi! Sonuc: {secilenDersNotu.SonucNotu}";
                    }
                    catch (Exception ex)
                    {
                        errorMessage = $"Bir hata olu�tu: {ex.Message}";
                    }
                }
                else
                {
                    errorMessage = "Yoklama durumu 1 veya 0 olmalidir!";

                }
            }
            else
            {
                errorMessage = "Ders notu 0-100 arasinda olmalidir!";
            }
        }
    }
}

