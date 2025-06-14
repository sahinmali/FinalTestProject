﻿using FinalTestProject.Services;
using FinalTestProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using FinalTestProject.Models;

namespace FinalTestProject.Components.Pages
{
    public partial class OgretimElemaniDersGor
    {
        [Inject] private UbysSystemDbContext DbContext { get; set; }

        [Inject] private SessionState SessionState { get; set; }

        public OgretimElemani? OgretimEleman { get; set; }

        public List<Ders>? TanimliDersler { get; set; } = new List<Ders>();

        public bool ShowCreate { get; set; }

        private string errorMessage = "";

        protected override async Task OnInitializedAsync()
        {
            if (!SessionState.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/", true);
            }

            ShowCreate = false;
            await GetOgrEleman();
        }

        public async Task GetOgrEleman()
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
                        await GetTanimliDersler(); // Metodun çağrılması async olarak yapılmalı
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

        public async Task GetTanimliDersler()
        {
            if (DbContext is not null && TanimliDersler != null)
            {
                TanimliDersler = await DbContext.Ders
                                .Where(d => d.OgretimElemaniTc.ToString() == OgretimEleman.TCKimlikNo.ToString())
                                .ToListAsync();
            }
        }
    }
}
