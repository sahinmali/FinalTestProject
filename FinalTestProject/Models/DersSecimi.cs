using FinalTestProject.Data;

namespace FinalTestProject.Models
{
    public class DersSecimi
    {
        public List<string>? secilenDersler { get; set; }
        public long KimlikNo { get; set; }
        public string? DanismanNo { get; set; }
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
