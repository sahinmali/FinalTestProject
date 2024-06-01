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
        public List<Hesap>? HesapList { get; set; }
        public List<Ogrenci>? OgrenciList { get; set; }
        public List<Danisman>? DanismanList { get; set; }
        public List<Idareci>? IdareciList { get; set; }
        public List<OgretimElemani>? OgretimElemaniList { get; set; }

        private string errorMessage = "";

        protected override async Task OnInitializedAsync()
        {
            ShowCreate = false;
            await GetHesapList();
        }

        public async Task GetHesapList()
        {
            _context ??= await UbysSystemDbContext.CreateDbContextAsync();

            if (_context is not null)
            {
                OgrenciList = await _context.Ogrenci.ToListAsync();
                OgretimElemaniList = await _context.OgretimElemani.ToListAsync();
                DanismanList = await _context.Danisman.ToListAsync();
                IdareciList = await _context.Idareci.ToListAsync();

                HesapList = OgrenciList.Cast<Hesap>()
                          .Concat(DanismanList.Cast<Hesap>())
                          .Concat(OgretimElemaniList.Cast<Hesap>())
                          .Concat(IdareciList.Cast<Hesap>())
                          .ToList();
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

        private Hesap ValidateCredentials(string userId, string password)
        {
            Hesap targetHesap = new();
            foreach (var acc in HesapList)
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