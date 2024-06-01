using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalTestProject.Models
{
    public class Ders
    {
        [Key]
        public string? DersKodu { get; set; }
        public string? DersAdi { get; set; }
        public int SaatCount { get; set; }
        public int Yariyil { get; set; }
        public float AKTS { get; set; }
        public float Kredi { get; set; }
        public List<string>? OgrenciList { get; set; }

        [NotMapped]
        public DersNotu DersinNotu { get; set; }
    }

}
