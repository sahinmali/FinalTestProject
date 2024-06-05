using FinalTestProject.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static FinalTestProject.Models.Constants;

namespace FinalTestProject.Models
{
    public class SecilenDers
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("DersSecimi")]
        public long TcKimlikNo { get; set; }

        [ForeignKey("Ders")]
        public string DersKodu { get; set; }
    }
}
