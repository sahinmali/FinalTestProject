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
        public int Yariyil { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; } // Checkbox se�imini takip etmek i�in
    }

}
