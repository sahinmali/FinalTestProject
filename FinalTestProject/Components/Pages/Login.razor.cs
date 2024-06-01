using FinalTestProject.Data;
using Microsoft.EntityFrameworkCore;
using static FinalTestProject.Models.Constants;

namespace FinalTestProject.Components.Pages
{
    public partial class Login
    {
        public string UserKimlikNo { get; set; }
        public string Password { get; set; }
        public bool ShowCreate { get; set; }
        private UbysSystemDbContext? _context;
        public List<Ogrenci>? OgrenciList { get; set; }

        private string errorMessage = "";

        protected override async Task OnInitializedAsync()
        {
            ShowCreate = false;
            await GetOgrenciList();
        }

        public async Task GetOgrenciList()
        {
            _context ??= await UbysSystemDbContext.CreateDbContextAsync();

            if (_context is not null)
            {
                OgrenciList = await _context.Ogrenci.ToListAsync();
            }
            if (_context is not null) await _context.DisposeAsync();
        }

        protected void LoginControl()
        {
            var credentials = ValidateCredentials(UserKimlikNo, Password);
            errorMessage = "";

            if (string.IsNullOrEmpty(UserKimlikNo) || string.IsNullOrEmpty(Password))
            {
                errorMessage = "Kimlik No ve Şifre giriniz.";
                return;
            }

            if (credentials.Ad != "")
            {
                if (credentials.UyelikType == UyelikType.Ogrenci)
                {
                    NavigationManager.NavigateTo("/OgrenciLayout");
                }
                else if (credentials.UyelikType == UyelikType.Danisman)
                {
                    NavigationManager.NavigateTo("/DanismanLayout");
                }
                else if (credentials.UyelikType == UyelikType.Idareci)
                {
                    NavigationManager.NavigateTo("/IdareciLayout");
                }
                else if (credentials.UyelikType == UyelikType.OgretimElemani)
                {
                    NavigationManager.NavigateTo("/OgretimElemaniLayout");
                }
            }
        }

        private Ogrenci ValidateCredentials(string userId, string password)
        {
            Ogrenci targetHesap = new();
            foreach (var acc in OgrenciList)
            {
                if (acc.TCKimlikNo.ToString() == userId && acc.Password.ToString() == password)
                {
                    targetHesap = acc;
                    break;
                }
            }
            return targetHesap;
        }
    }
}