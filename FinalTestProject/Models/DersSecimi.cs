
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalTestProject.Models
{
    public class DersSecimi
    {
        public List<string>? SecilenDersler { get; set; }
        [Key]
        public long OgrenciKimlikNo { get; set; }

        [ForeignKey("Danisman")]
        public string? DanismanKimlikNo { get; set; }

        public List<Ders> DersList = [];

    }
}
