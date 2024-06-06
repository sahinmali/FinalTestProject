using FinalTestProject.Services;
using FinalTestProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using FinalTestProject.Models;


namespace FinalTestProject.Components.Pages
{
    public partial class IdareciDanismanAtama
    {
        [Inject] private UbysSystemDbContext DbContext { get; set; }
        [Inject] private SessionState SessionState { get; set; }

        public List<Danisman>? DanismanList { get; set; } = new List<Danisman>();
        public Danisman? SecilenDanisman { get; set; }

        public List<Ogrenci>? OgrList { get; set; } = new List<Ogrenci>();
        public bool ShowCreate { get; set; }

        private string errorMessage;

        protected override async Task OnInitializedAsync()
        {
            if (!SessionState.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/", true);
            }

            ShowCreate = false;
            await GetDanismanList();
        }

        private async Task GetDanismanList()
        {
            if (SessionState.AssignedHesap is not null && DbContext is not null)
            {
                try
                {
                    DanismanList = await DbContext.Danisman.ToListAsync();
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

        private async Task GetDanisman(ChangeEventArgs e)
        {
            var secilenDanismanTc = e.Value?.ToString();

            if (string.IsNullOrEmpty(secilenDanismanTc))
            {
                secilenDanismanTc = null;
                errorMessage = "Lütfen geçerli bir danisman seçiniz.";
                return;
            }

            SecilenDanisman = DanismanList.FirstOrDefault(d => d.TCKimlikNo.ToString() == secilenDanismanTc); ;

            if (SecilenDanisman == null)
            {
                errorMessage = "SecilenDanisman null!.";
            }
            else
            {
                await GetOgrList();
            }
        }

        private async Task GetOgrList()
        {
            if (DbContext is not null)
            {
                OgrList = await DbContext.Ogrenci.Include(o => o.OgrDanisman).ToListAsync();
            }
        }

        private async Task UpdateOgr()
        {
            var secildi_mi = OgrList.Any(o => o.IsSelected);
            if (secildi_mi == null)
            {
                errorMessage = "Danisman atanacak ogrenci seçilmedi.";
                return;
            }

            var secilen_ogrler = OgrList.Where(o => o.IsSelected).ToList();

            if (secilen_ogrler != null && secilen_ogrler.Count > 0)
            {
                try
                {
                    foreach (var ogrenci in secilen_ogrler)
                    {
                        ogrenci.OgrenciDanismani = SecilenDanisman.TCKimlikNo;
                    }
                    // Ders notunu veritabanında güncelle
                    DbContext.Ogrenci.UpdateRange(secilen_ogrler);
                    await DbContext.SaveChangesAsync();

                    errorMessage = "Danismani atanan ogrenciler: ";

                    foreach (var ogrenci in secilen_ogrler)
                    {
                        errorMessage += $"\n{ogrenci.TCKimlikNo} Tc numarali {ogrenci.Ad}'nin danismani {ogrenci.OgrDanisman.Ad} olarak başarıyla güncellendi!";
                    }

                    //isselectedi tekrar false yap

                    foreach (var ogrenci in secilen_ogrler)
                    {
                        ogrenci.IsSelected = false;
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = $"Bir hata oluştu: {ex.Message}";
                }               
            }
            else
            {
                errorMessage = "Bir ogrenci secimi yapilmamis!";
            }
        }
    }
}
