namespace FinalTestProject.Models.Accounts
{
    public class OgretimElemani : Hesap
    {
        public List<Ders> SecilmisDersler = new List<Ders>();
        //public List<Sinav> Sinavlar = new List<Sinav>();

        public List<Ders> derslerimiGor()
        {
            return SecilmisDersler;
        }

        public int GetTotalSaatCount()
        {
            int totalSaatCount = 0;
            foreach (var item in SecilmisDersler)
            {
                totalSaatCount += item.SaatCount;
            }
            return totalSaatCount;
        }

        //bunu tekrar dusun
        public void VizeNotGir(float not, DersNotu ders_not)
        {
            ders_not.setAraSinav(not);
        }

        public void FinalNotGir(float not, DersNotu ders_not)
        {
            ders_not.setFinal(not);
        }
    }

}
