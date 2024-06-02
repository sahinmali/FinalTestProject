using FinalTestProject.Services;
using FinalTestProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;

namespace FinalTestProject.Components.Pages
{
    public partial class DanismanOgrGor
    {
        [Inject] private UbysSystemDbContext DbContext { get; set; }

        [Inject] private SessionState SessionState { get; set; }

        public Danisman? Danisman { get; set; }
        public string Danisman_Adi { get; set; }
        public List<Ogrenci> TanimliOgrenciler { get; set; } = new List<Ogrenci>();

        public bool ShowCreate { get; set; }

        private UbysSystemDbContext? _context;

        private string errorMessage = "";
        public string deneme = "";

        protected override async Task OnInitializedAsync()
        {
            ShowCreate = false;
            await GetDanisman();
        }

        public async Task GetDanisman()
        {
            if (SessionState.UserTCKimlikNo is not null && DbContext is not null)
            {
                try
                {
                    Danisman = await DbContext.Danisman
                        .FirstOrDefaultAsync(d => d.TCKimlikNo.ToString() == SessionState.UserTCKimlikNo);

                    if (Danisman is not null)
                    {
                        Danisman_Adi = Danisman.Ad;
                        // Populate other properties if needed
                        await GetTanimliOgrler(); // Metodun çağrılması async olarak yapılmalı
                    }
                    else
                    {
                        errorMessage = "Danisman not found";
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = $"Error fetching Danisman: {ex.Message}";
                }
            }
            else
            {
                errorMessage = "Invalid session state or DbContext not available";
            }
        }

        public async Task GetTanimliOgrler()
        {
            if (DbContext is not null)
            {
                deneme += "";
                TanimliOgrenciler = await DbContext.Ogrenci
                                .Where(o => o.OgrenciDanismani.ToString() == Danisman.TCKimlikNo.ToString())
                                .ToListAsync();
            }
        }
    }
}
