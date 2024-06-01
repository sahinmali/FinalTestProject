using FinalTestProject.Models;

namespace FinalTestProject.Data
{
    public class Idareci : Hesap
    {
        public List<string>? OgretimElemaniList { get; set; }
        public List<string>? DersListe { get; set; }
        public List<string>? AcilanDersler { get; set; }

        public Ders DersAc(string ders_kodu, int saat, int yari_yil, int akts, int kredi, string ders_adi)
        {
            var ders = new Ders()
            {
                DersKodu = ders_kodu,
                SaatCount = saat,
                Yariyil = yari_yil,
                AKTS = akts,
                Kredi = kredi,
                DersAdi = ders_adi
            };

            if (ders != null)
            {
                AcilanDersler.Add(ders.DersKodu);
            }
            return ders!;
        }

        public void DersKapa(Ders ders)
        {
            AcilanDersler.Remove(ders.DersKodu);
        }

        // public void DersEslestir(OgretimElemani eleman, Ders ders)
        // {
        //     if (eleman.GetTotalSaatCount() < ValidValues.LimitSaatCount)
        //     {
        //         if (SaatAsildiMi(eleman, ders)) return;

        //         eleman.SecilmisDersler.Add(ders);
        //     }
        // }

        // private static bool SaatAsildiMi(OgretimElemani eleman, Ders ders)
        // {
        //     if (ders.SaatCount + eleman.GetTotalSaatCount() > ValidValues.LimitSaatCount)
        //     {
        //         Console.WriteLine("Ogretim Elemani'nin saat limiti asiliyor.");
        //         return true;
        //     }
        //     return false;
        // }
    }

}
