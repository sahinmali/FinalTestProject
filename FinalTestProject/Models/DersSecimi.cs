using FinalTestProject.Models.Accounts;

namespace FinalTestProject.Models
{
    public class DersSecimi
    {
        public List<Ders> secilenDersler = new List<Ders>();
        public long ogr_no ;
        public Danisman onaylayan = new Danisman();
        public bool onay;


        //public DersSecimi(List<Ders> sd, Danisman dansiman) //ogrenci de bir parametre olarak verilebilir.
        //{
        //    this.secilenDersler = sd;
        //    this.onaylayan = dansiman;
        //}

        public void onayla()
        {
            onay = true;
        }

    }
}
