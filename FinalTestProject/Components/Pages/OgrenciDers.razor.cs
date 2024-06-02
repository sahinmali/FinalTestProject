using FinalTestProject.Services;
using FinalTestProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using FinalTestProject.Models;

namespace FinalTestProject.Components.Pages
{
    public partial class OgrenciDers
    {
        [Inject] private UbysSystemDbContext DbContext { get; set; }

        [Inject] private SessionState SessionState { get; set; }

        public Ogrenci? Ogrenci { get; set; }

        public List<DersNotu>? TanimliDersNotlari { get; set; } = new List<DersNotu>();
        public bool ShowCreate { get; set; }

        private string errorMessage = "";

        protected override async Task OnInitializedAsync()
        {
            ShowCreate = false;
            await GetOgrenci();
        }

        public async Task GetOgrenci()
        {
            if (SessionState.UserTCKimlikNo is not null && DbContext is not null)
            {
                try
                {
                    Ogrenci = await DbContext.Ogrenci
                        .FirstOrDefaultAsync(d => d.TCKimlikNo.ToString() == SessionState.UserTCKimlikNo);

                    if (Ogrenci is not null)
                    {
                        // Populate other properties if needed
                        await GetTanimliDerslNotlari(); // Metodun çağrılması async olarak yapılmalı
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

        public async Task GetTanimliDerslNotlari()
        {
            if (DbContext is not null && TanimliDersNotlari != null)
            {
                TanimliDersNotlari = await DbContext.DersNotu
                                .Include(dn => dn.Ders) // Ders bilgilerini dahil et
                                .Where(o => o.OgrenciTc.ToString() == Ogrenci.TCKimlikNo.ToString())
                                .ToListAsync();
            }

            if(TanimliDersNotlari != null)
            {
                foreach (var ders_not in  TanimliDersNotlari)
                {
                    ders_not.setAraSinav(ders_not.AraSinav.ogr_not);
                    ders_not.setFinal(ders_not.FinalSinav.ogr_not);

                    ders_not.HesaplaGecmeNotu();
                }
            }
        }
    }
}
