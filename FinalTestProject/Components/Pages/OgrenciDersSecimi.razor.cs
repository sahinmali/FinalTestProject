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
        [Inject] private UbysSystemDbContext DbContext { get; set; }

        private DersSecimi DersSecimi { get; set; }
        private Ogrenci AssignedOgrenci { get; set; }

        private List<Ders>? TanimliDersler { get; set; } = [];
        private string errorMessage;

        protected override async Task OnInitializedAsync()
        {
            DbContext ??= await UbysSystemDbContext.CreateDbContextAsync();

            await SetDersler();
        }

        private async Task SetDersler()
        {
            if (DbContext is not null)
            {
                AssignedOgrenci = SessionState.AssignedHesap as Ogrenci;

                TanimliDersler = await DbContext.Ders
                    .Where(d => d.Yariyil == AssignedOgrenci.Yariyil)
                    .ToListAsync();

                TanimliDersler.ForEach(d =>
                {
                    Console.WriteLine(d.DersAdi);
                });
            }
        }
    }
}