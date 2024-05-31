
namespace FinalTestProject.Models.Accounts
{
    public class Ogrenci : Hesap
    {
        public List<Ders> SecilmisDersler = [];
        public Danisman ogrDanisman = new();
        public int donem;
        public int sinif;
        public DersSecimi? dersSecim;

        public DersSecimi DersSecimYap(List<Ders> dersler)
        {
            var secim = new DersSecimi()
            {
                secilenDersler = dersler,
                onaylayan = this.ogrDanisman,
            };

            return secim;
        }
    }

}
