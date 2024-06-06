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

        private string errorMessage;

        protected override async Task OnInitializedAsync()
        {
            if (!SessionState.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/", true);
            }

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

                
                if (DersList == null)
                {
                    DersList = new List<Ders>();
                }
            }
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
            if (selectedOE.SecilmisDersler.Contains(newDers) || !IsDersExist(newDers)) return;
            Ders targetDers = GetDers(newDers);
            if (SaatAsildiMi(targetDers))
            {
                errorMessage = $"Ogretim elemaninin haftalik ders saati 20'yi  asmamalidir! Ogretim Elemaninin guncel haftalik saati: {GetTotalSaatCount()}";
                return; 
            }

            _context ??= await UbysSystemDbContext.CreateDbContextAsync();
            if (_context is not null)
            {
                selectedOE.SecilmisDersler.Add(newDers);
                targetDers.OgretimElemaniTc = selectedOE.TCKimlikNo;
                if (selectedOE is not null)
                {
                    _context.OgretimElemani.Update(selectedOE);
                    _context.Ders.Update(targetDers);
                }
                newDers = ""; // Clear the input after adding
                EditRecord = false;
                await _context.SaveChangesAsync();
            }
        }

        private bool SaatAsildiMi(Ders targetDers)
        {
            return GetTotalSaatCount() + targetDers.SaatCount > ValidValues.LimitSaatCount;
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

        private Ders GetDers(string DersKodu)
        {
            return DersList?.FirstOrDefault(h => h.DersKodu == DersKodu);
        }

        private int GetTotalSaatCount()
        {
            int totalSaatCount = 0;
            List<Ders> dersler = GetDersList(selectedOE);
            foreach (var item in dersler)
            {
                if (item != null)
                {
                    totalSaatCount += item.SaatCount;
                }
            }
            return totalSaatCount;
        }

        private List<Ders> GetDersList(OgretimElemani oe)
        {
            List<Ders> atanmisDersler = [];

            foreach (var item in oe.SecilmisDersler)
            {
                Ders ders = GetDers(item); 
                if (ders != null)
                {
                    atanmisDersler.Add(ders);
                }
            }
            return atanmisDersler;
        }

        
        //public void VizeNotGir(float not, DersNotu ders_not)
        //{
        //    ders_not.setAraSinav(not);
        //}

        //public void FinalNotGir(float not, DersNotu ders_not)
        //{
        //    ders_not.setFinal(not);
        //}
    }
}