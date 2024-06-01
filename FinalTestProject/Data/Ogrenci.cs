using System.ComponentModel.DataAnnotations.Schema;
using FinalTestProject.Models;

namespace FinalTestProject.Data
{
    public class Ogrenci : Hesap
    {
        public List<string>? SecilmisDersler { get; set; }
        public string? OgrenciDanismani { get; set; }
        public int? Sinif { get; set; }
        public int? Donem { get; set; }

        //[NotMapped]
        //public DersSecimi dersSecim = new();

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
