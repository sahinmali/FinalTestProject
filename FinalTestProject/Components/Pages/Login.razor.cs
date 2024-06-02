using FinalTestProject.Data;
using FinalTestProject.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using static FinalTestProject.Models.Constants;

namespace FinalTestProject.Components.Pages
{
    public partial class Login
    {
        [Inject] private SessionState SessionState { get; set; }

        public string UserKimlikNo { get; set; }
        public string Password { get; set; }
        private UbysSystemDbContext? _context;
        public List<Hesap>? HesapList { get; set; }
        public List<Ogrenci>? OgrenciList { get; set; }
        public List<Danisman>? DanismanList { get; set; }
        public List<Idareci>? IdareciList { get; set; }
        public List<OgretimElemani>? OgretimElemaniList { get; set; }

        private string errorMessage = "";

        protected override async Task OnInitializedAsync()
        {
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

        protected async Task LoginControl()
        {
            var credentials = ValidateCredentials(UserKimlikNo, Password);
            errorMessage = "";

            if (string.IsNullOrEmpty(UserKimlikNo) || string.IsNullOrEmpty(Password))
            {
                errorMessage = "Kimlik No ve Sifre giriniz.";
                return;
            }

            if (credentials != null && credentials.Ad != "")
            {
                try
                {
                    SessionState.UserTCKimlikNo = credentials.TCKimlikNo.ToString();
                    SessionState.UserType = credentials.UyelikType.ToString();
                    SessionState.Ad = credentials.Ad;
                }
                catch (Exception ex)
                {
                    errorMessage = "Session islemi sirasinda bir hata olustu: " + ex.Message;
                    return;
                }

                string navigateTo = "";

                switch (credentials.UyelikType)
                {
                    case UyelikType.Ogrenci:
                        navigateTo = "/OgrenciLayout";
                        break;
                    case UyelikType.Danisman:
                        navigateTo = "/DanismanLayout";
                        break;
                    case UyelikType.Idareci:
                        navigateTo = "/IdareciLayout";
                        break;
                    case UyelikType.OgretimElemani:
                        navigateTo = "/OgretimElemaniLayout";
                        break;
                }

                if (!string.IsNullOrEmpty(navigateTo))
                {
                    try
                    {
                        NavigationManager.NavigateTo(navigateTo);
                    }
                    catch (Exception ex)
                    {
                        errorMessage = "Navigation islemi sirasinda bir hata olustu: " + ex.Message;
                    }
                }
            }
            else
            {
                errorMessage = "Girilen bilgiler gecersiz.";
            }
        }

        private Hesap ValidateCredentials(string userId, string password)
        {
            Hesap targetHesap = null;

            foreach (var acc in HesapList)
            {
                if (acc.TCKimlikNo.ToString() == userId && acc.Password == password)
                {
                    targetHesap = acc;
                    break;
                }
            }

            return targetHesap;
        }
    }
}