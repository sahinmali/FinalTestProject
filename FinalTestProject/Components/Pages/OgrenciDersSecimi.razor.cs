using FinalTestProject.Models;
using FinalTestProject.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace FinalTestProject.Components.Pages
{
    public partial class OgrenciDersSecimi
    {
        [Inject] private UbysSystemDbContext DbContext { get; set; }

        private List<Ders>? TanimliDersler { get; set; } = [];

        protected override async Task OnInitializedAsync()
        {
            DbContext ??= await UbysSystemDbContext.CreateDbContextAsync();

            await SetDersler();
        }

        private async Task SetDersler()
        {
            if (DbContext is not null)
            {
                TanimliDersler = await DbContext.Ders.ToListAsync();
            }
        }
    }
}