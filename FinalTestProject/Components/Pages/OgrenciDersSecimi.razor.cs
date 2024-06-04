using FinalTestProject.Data;
using FinalTestProject.Models;
using FinalTestProject.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace FinalTestProject.Components.Pages
{
    public partial class OgrenciDersSecimi
    {
        [Inject] private SessionState SessionState { get; set; }
        [Inject] private UbysSystemDbContext _context { get; set; }

        private DersSecimi DersSecimi { get; set; }
        private Ogrenci AssignedOgrenci { get; set; }

        private List<Ders>? TanimliDersler { get; set; } = [];
        private string errorMessage;

        private string ButtonText = "Add ";

        private bool IsAKTSExceeded = false;
        private bool IsSecimSent = false;

        protected override async Task OnInitializedAsync()
        {
            _context ??= await UbysSystemDbContext.CreateDbContextAsync();

            await SetDersler();
        }

        private async Task SetDersler()
        {
            if (_context is not null)
            {
                AssignedOgrenci = SessionState.AssignedHesap as Ogrenci;

                TanimliDersler = await _context.Ders.Where(d => d.Yariyil == AssignedOgrenci.Yariyil).ToListAsync();
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
            TanimliDersler.Remove(ders);
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

        private async Task OnClickConfirmDersSecimi()
        {
            if (_context is not null)
            {
                _context.DersSecimi.Add(DersSecimi);
                IsSecimSent = true;
            }
            await _context.SaveChangesAsync();
            DersSecimi = null;
        }

        private void RemoveDersFromSecim(Ders ders) 
        {
            TanimliDersler.Add(ders);
            DersSecimi.DersList.Remove(ders);
            DersSecimi.SecilenDersler.Remove(ders.DersKodu);
            ders.IsDersAdded = false;
        }
    }
}