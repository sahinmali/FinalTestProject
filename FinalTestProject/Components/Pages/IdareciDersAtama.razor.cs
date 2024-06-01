using FinalTestProject.Data;
using FinalTestProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalTestProject.Components.Pages
{
    public partial class IdareciDersAtama
    {
        private UbysSystemDbContext? _context;
        public List<OgretimElemani>? OgretimElemaniList { get; set; }
        public List<Ders>? DersList { get; set; }
        private OgretimElemani? selectedOE;
        private int? selectedOEKimlikNo;
        private string buttonText = "Ogretim Elemani";
        private bool _IsOESelected = false;

        public long EditingId { get; private set; }
        public bool EditRecord { get; set; }
        private string newDers = "";

        protected override async Task OnInitializedAsync()
        {
            _IsOESelected = false;
            await GetHesapList();
        }

        public async Task GetHesapList()
        {
            _context ??= await UbysSystemDbContext.CreateDbContextAsync();

            if (_context is not null)
            {
                OgretimElemaniList = await _context.OgretimElemani.ToListAsync();

                DersList = await _context.Ders.ToListAsync();
            }
            if (_context is not null) await _context.DisposeAsync();
        }

        public void OnHesapSelected(long TCKimlikNo)
        {
            selectedOEKimlikNo = Convert.ToInt32(TCKimlikNo);
            selectedOE = OgretimElemaniList.FirstOrDefault(h => h.TCKimlikNo == selectedOEKimlikNo);
            buttonText = selectedOE.Ad;
            _IsOESelected = true;
        }

        public async Task UpdateOE()
        {
            Console.WriteLine(selectedOE);
            if (selectedOE.SecilmisDersler.Contains(newDers) || !IsDersExist(newDers)) return;
            _context ??= await UbysSystemDbContext.CreateDbContextAsync();

            if (_context is not null)
            {
                selectedOE.SecilmisDersler.Add(newDers);
                newDers = ""; // Clear the input after adding
                if (selectedOE is not null) _context.OgretimElemani.Update(selectedOE);
                EditRecord = false;
                await _context.SaveChangesAsync();
            }
            if (_context is not null) await _context.DisposeAsync();
        }

        private bool IsDersExist(string targetDersKodu)
        {
            bool isExist = false;
            foreach (var ders in DersList)
            {
                if (ders.DersKodu == targetDersKodu)
                {
                    isExist = true;
                    break;
                }
            }
            return isExist;
        }
    }
}