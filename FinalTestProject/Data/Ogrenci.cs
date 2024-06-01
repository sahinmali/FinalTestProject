using FinalTestProject.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalTestProject.Data
{
    public class Ogrenci : Hesap
    {
        public List<string>? SecilmisDersler { get; set; }

        [ForeignKey("Danisman")]
        public long? OgrenciDanismani { get; set; }
        public Danisman OgrDanisman { get; set; }
        public int? Sinif { get; set; }
        public int? Donem { get; set; }

        public DersSecimi dersSecim = new();

        // public DersSecimi DersSecimYap(List<Ders> dersler)
        // {
        //     var secim = new DersSecimi()
        //     {
        //         secilenDersler = dersler,
        //         onaylayan = this.OgrenciDanismani,
        //     };

        //     return secim;
        // }
    }

}
